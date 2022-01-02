using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<IEnumerable<UserInfoDto>> GetUsersAsync()
    {
      return await _context.Users
        .ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }

    public async Task<UserInfoDto> GetUserAsync(int id)
    {
      return await _context.Users
        .Where(user => user.Id == id)
        .ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<UserInfoDto> GetUserAsync(string username)
    {
      return await _context.Users
        .ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync(user => user.Username == username.ToLower());
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
      _context.Entry(user).State = EntityState.Modified;
    }

  }
}