using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface ICongregationRepository
  {
    void AddCongregation(Congregation congregation);
    void DeleteCongregation(Congregation congregation);
    Task<IEnumerable<Congregation>> GetCongregationsAsync();
    Task<Congregation> GetCongregationAsync(int id);
    Task<Congregation> GetCongregationAsync(string name);
    void Update(Congregation congregation);
    Task<bool> SaveAllAsync();
  }
}