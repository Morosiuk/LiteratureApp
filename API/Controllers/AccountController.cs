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

      user.UserName = registerDto.Username.ToLower();
      user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
      user.PasswordSalt = hmac.Key;      

      //Set publisher role
      var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.Default);
      user.AddRole(defaultRole, registerDto.Congregation);

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return new UserDto
      {
        Username = user.UserName,
        Token = _tokenService.CreateToken(user),
        CongregationId = user.CongregationRoles?.FirstOrDefault()?.CongregationId ?? 0,
        Firstname = user.FirstName,
        Surname = user.Surname
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
        if (computedHash[i] != user.Password[i]) return Unauthorized("Invalid password");
      }

      var roles = (await _context.Users
        .Include(u => u.CongregationRoles)
        .FirstOrDefaultAsync(u => u.Id == user.Id))
        .CongregationRoles;

      return new UserDto
      {
        Username = user.UserName,
        Token = _tokenService.CreateToken(user),
        CongregationId = roles?.FirstOrDefault()?.CongregationId ?? 0
      };
    }

    private async Task<bool> UserExists(string username)
    {
      return await _context.Users
        .AnyAsync(user => user.UserName == username.ToLower());
    }
  }
}