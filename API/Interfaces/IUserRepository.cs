using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface IUserRepository
  {
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<UserInfoDto>> GetUsersAsync();
    Task<UserInfoDto> GetUserAsync(int id);
    Task<UserInfoDto> GetUserAsync(string username);
  }
}