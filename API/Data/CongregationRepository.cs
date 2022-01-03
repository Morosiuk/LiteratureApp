using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class CongregationRepository : ICongregationRepository
  {
    private readonly DataContext _context;
    public CongregationRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Congregation>> GetCongregationsAsync()
    {
      return await _context.Congregations.ToListAsync(); 
    }

    public async Task<Congregation> GetCongregationAsync(int id)
    {
      return await _context.Congregations.FindAsync(id);
    }

    public async Task<Congregation> GetCongregationAsync(string name)
    {
      return await _context.Congregations
        .SingleOrDefaultAsync(cong => cong.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(Congregation congregation)
    {
      _context.Entry(congregation).State = EntityState.Modified;
    }
  }
}