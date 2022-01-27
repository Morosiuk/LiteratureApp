using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    public AccountController(DataContext context, ITokenService tokenService, IMapper mapper)
    {
      _mapper = mapper;
      _tokenService = tokenService;
      _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      if (await UserExists(registerDto.Username)) return BadRequest("Username already exists");

      var user = _mapper.Map<AppUser>(registerDto);
      using var hmac = new HMACSHA512();

      //Create AppUser
      user.UserName = registerDto.Username.ToLower();
      user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
      user.PasswordSalt = hmac.Key;      
      _context.Users.Add(user);

      //Create Publisher role
      var publisher = await _context.Publishers
        .FirstOrDefaultAsync(p => 
          p.CongregationId == registerDto.Congregation &&
          p.FirstName.ToLower() == registerDto.Firstname.ToLower() &&
          p.Surname.ToLower() == registerDto.Surname.ToLower());
      if (publisher == null)
      {
        publisher = new Publisher{ 
          FirstName = registerDto.Firstname,
          Surname = registerDto.Surname,
          CongregationId = registerDto.Congregation
        };
      }
      _context.Publishers.Add(publisher);

      user.AssignPublisher(publisher);
      await _context.SaveChangesAsync();

      return new UserDto
      {
        Username = user.UserName,
        Token = _tokenService.CreateToken(user)
      };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Register(LoginDto userDetails)
    {
      var user = await _context.Users
        .FirstOrDefaultAsync(user => user.UserName == userDetails.Username.ToLower());

      if (user == null) return Unauthorized("Invalid username");
      using var hmac = new HMACSHA512(user.PasswordSalt);
      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDetails.Password));

      for (int i = 0; i < computedHash.Length; i++)
      {
        if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
      }

      var userToReturn = _mapper.Map<UserDto>(user);
      userToReturn.Token = _tokenService.CreateToken(user);
      return userToReturn;
    }

    private async Task<bool> UserExists(string username)
    {
      return await _context.Users
        .AnyAsync(user => user.UserName == username.ToLower());
    }
  }
}