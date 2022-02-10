using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Helpers.Params;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class UsersController : BaseApiController
  {
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepo, IMapper mapper)
    {
      _mapper = mapper;
      _userRepo = userRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserInfoDto>>> GetUsersAsync([FromQuery]UserParams userParams)
    {
      var users = await _userRepo.GetUsersAsync(userParams);      
      Response.AddPaginationHeader(users.CurrentPage, users.PageSize, 
        users.TotalCount, users.TotalPages);

      var usersToReturn = _mapper.Map<IEnumerable<UserInfoDto>>(users);
      return Ok(usersToReturn);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserInfoDto>> GetUserAsync(int Id)
    {
      var user = await _userRepo.GetUserAsync(Id);
      var userToReturn = _mapper.Map<UserInfoDto>(user);
      return userToReturn;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UserInfoDto>> GetUserAsync(string username)
    {
      var user = await _userRepo.GetUserAsync(username);
      return user;
    }
  }
}