using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
  public class LiteratureRepository : ILiteratureRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public LiteratureRepository(DataContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public LanguageCode AddLanguageCode(LanguageCodeDto code)
    {
      var newCode = _mapper.Map<LanguageCode>(code);
      _context.LanguageCodes.Add(newCode);
      return newCode;
    }

    public Literature AddLiterature(LiteratureDto literature)
    { 
      var newLiterature = _mapper.Map<Literature>(literature);
      _context.Literature.Add(newLiterature);
      return newLiterature;
    }

    public void DeleteLanguageCode(LanguageCode code)
    {
      _context.LanguageCodes.Remove(code);
    }

    public void DeleteLiterature(Literature literature)
    {
      _context.Literature.Remove(literature);
    }

    public async Task<ICollection<LanguageCode>> GetLanguageCodesAsync(string language)
    {
      return await _context.LanguageCodes
        .Where(code => EF.Functions.Like(code.Language, $"%{language}%"))
        .ToListAsync();
    }

    public async Task<LanguageCode> GetLanguageCodeAsync(string code)
    {
      return await _context.LanguageCodes.FirstOrDefaultAsync(c => c.Code.ToLower() == code.ToLower());
    }

    public async Task<ICollection<LanguageCode>> GetLanguageCodesAsync()
    {
      return await _context.LanguageCodes.ToListAsync();
    }

    public async Task<ICollection<Literature>> GetLiteratureAsync(string name)
    {
      return await _context.Literature
        .Where(lit => EF.Functions.Like(lit.Name, $"%{name}%"))
        .ToListAsync();
    }

    public async Task<Literature> GetLiteratureAsync(int id)
    {
      return await _context.Literature.FindAsync(id);
    }

    public async Task<ICollection<Literature>> GetLiteratureAsync()
    {
      return await _context.Literature.ToListAsync();
    }

    public async Task<Literature> GetLiteratureBySymbolAsync(string symbol)
    {
      return await _context.Literature
        .FirstOrDefaultAsync(lit => lit.Symbol.ToLower() == symbol.ToLower());
    }

    public async Task<Literature> GetLiteratureByItemAsync(int itemId)
    {
      return await _context.Literature
        .FirstOrDefaultAsync(lit => lit.ItemId == itemId);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void UpdateLanguageCode(LanguageCode code)
    {
      _context.Entry(code).State = EntityState.Modified;
    }

    public void UpdateLiterature(Literature literature)
    {
      _context.Entry(literature).State = EntityState.Modified;
    }

    private async Task<bool> LiteratureExists(LiteratureDto literatureDto)
    {
      //Check symbol is unique
      var existingSymbol = await _context.Literature
        .AnyAsync(lit => lit.Symbol == literatureDto.Symbol.ToLower());
      if (existingSymbol) return true;

      //Check ItemId is unique (if provided)
      if (literatureDto.ItemId != null) 
      {
        var existingItem = await _context.Literature
          .AnyAsync(lit => lit.ItemId == literatureDto.ItemId);
        if (existingItem) return true;
      }
      return false;
    }

    public async Task<LanguageCode> GetLanguageCodeAsync(int id)
    {
      return await _context.LanguageCodes.FindAsync(id);
    }
  }
}
