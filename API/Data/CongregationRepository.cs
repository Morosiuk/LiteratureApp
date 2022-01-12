using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class CongregationRepository : ICongregationRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CongregationRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
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

    public void AddCongregation(Congregation congregation)
    {
      _context.Congregations.Add(congregation);
    }

    public void Update(Congregation congregation)
    {
      _context.Entry(congregation).State = EntityState.Modified;
    }

    public void DeleteCongregation(Congregation congregation)
    {
      throw new System.NotImplementedException();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}