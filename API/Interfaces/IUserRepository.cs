using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Helpers.Params;

namespace API.Interfaces
{
  public interface IUserRepository
  {
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<PagedList<UserInfoDto>> GetUsersAsync(UserParams userParams);
    Task<AppUser> GetUserAsync(int id);
    Task<UserInfoDto> GetUserAsync(string username);
  }
}