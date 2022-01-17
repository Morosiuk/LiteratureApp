using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
  public interface ICongregationRepository
  {
    void AddCongregation(Congregation congregation);
    void DeleteCongregation(Congregation congregation);
    Task<PagedList<CongregationDto>> GetCongregationsAsync(CongParams congParams);
    Task<Congregation> GetCongregationAsync(int id);
    Task<Congregation> GetCongregationAsync(string name);
    void Update(Congregation congregation);
    Task<bool> SaveAllAsync();
  }
}