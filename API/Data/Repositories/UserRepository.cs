using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
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

    public async Task<PagedList<UserInfoDto>> GetUsersAsync(UserParams userParams)
    {
      var query = _context.Users
        .AsNoTracking()
        .AsQueryable();
      
      if (userParams.Congregation > 0)
      {
        //Filter on congregation
        query = query
          .Where(user => user.AssignedPublishers
            .Any(pub => pub.Publisher.CongregationId == userParams.Congregation)
          );
      }
      query = userParams.OrderBy switch
      {
        "username" => query.OrderBy(user => user.UserName),
        "lastActive" => query.OrderByDescending(user => user.LastActive),
        _ => query.OrderBy(user => user.Id)
      };

      return await PagedList<UserInfoDto>.CreateAsync(
        query.ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider), 
        userParams.PageNumber, 
        userParams.PageSize);
    }

    public async Task<AppUser> GetUserAsync(int id)
    {
      return await _context.Users
        .Where(user => user.Id == id)
        .SingleOrDefaultAsync();
    }

    public async Task<UserInfoDto> GetUserAsync(string username)
    {
      var user = await _context.Users
        .ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync(user => user.Username == username.ToLower());
      return user;
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