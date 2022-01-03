using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
  public interface ICongregationRepository
  {
    void Update(Congregation congregation);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<Congregation>> GetCongregationsAsync();
    Task<Congregation> GetCongregationAsync(int id);
    Task<Congregation> GetCongregationAsync(string name);
  }
}