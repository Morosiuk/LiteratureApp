using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Helpers.Params;
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

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    //########################################
    //##############LITERATURE################
    //########################################

    public async Task<Literature> GetLiteratureAsync(int id)
    {
      return await _context.Literature.FindAsync(id);
    }

    public async Task<Literature> GetLiteratureAsync(string symbol)
    {
      return await _context.Literature
        .FirstOrDefaultAsync(lit => lit.Symbol.ToLower() == symbol.ToLower());
    }

    public async Task<Literature> GetLiteratureFromItemAsync(int itemId)
    {
      return await _context.Literature
        .FirstOrDefaultAsync(lit => lit.ItemId == itemId);
    }

    public async Task<PagedList<Literature>> GetLiteratureAsync(LiteratureParams litParams)
    {
      var query = _context.Literature
        .AsNoTracking()
        .AsQueryable();

      query = query.Where(lit => 
        EF.Functions.Like(lit.FullName, $@"%{litParams.Keyword}%") ||
        EF.Functions.Like(lit.Name, $@"%{litParams.Keyword}%"));
      query = query.Where(lit => EF.Functions.Like(lit.Symbol, $@"%{litParams.Symbol}%"));
      
      query = litParams.OrderBy switch
      {
        "symbol" => query.OrderBy(lit => lit.Symbol),
        "fullName" => query.OrderBy(lit => lit.FullName),
        _ => query.OrderBy(lit => lit.Name)
      };

      return await PagedList<Literature>.CreateAsync(
        query, 
        litParams.PageNumber,
        litParams.PageSize);
    }

    private async Task<bool> LiteratureExists(LiteratureDto literatureDto)
    {
      //Check symbol is unique
      var existingSymbol = await GetLiteratureAsync(literatureDto.Symbol);
      if (existingSymbol != null) return true;

      //Check ItemId is unique (if provided)
      if (literatureDto.ItemId != null) 
      {
        var existingItem = await _context.Literature
          .AnyAsync(lit => lit.ItemId == literatureDto.ItemId);
        if (existingItem) return true;
      }
      return false;
    }

    public Literature AddLiterature(LiteratureDto literature)
    { 
      var newLiterature = _mapper.Map<Literature>(literature);
      _context.Literature.Add(newLiterature);
      return newLiterature;
    }    

    public void UpdateLiterature(Literature literature)
    {
      _context.Entry(literature).State = EntityState.Modified;
    }

    public void DeleteLiterature(Literature literature)
    {
      _context.Literature.Remove(literature);
    }

    //########################################
    //############LANGAUGE CODES##############
    //########################################

    public async Task<LanguageCode> GetLanguageCodeAsync(int id)
    {
      return await _context.LanguageCodes.FindAsync(id);
    }

    public async Task<LanguageCode> GetLanguageCodeAsync(string code)
    {
      return await _context.LanguageCodes
        .FirstOrDefaultAsync(c => c.Code.ToLower() == code.ToLower());
    }

    public async Task<PagedList<LanguageCode>> GetLanguageCodesAsync(
      LanguageParams languageParams)
    {
      var query = _context.LanguageCodes
        .AsNoTracking()
        .AsQueryable();

      query = query.Where(lang => 
        EF.Functions.Like(lang.Language, $@"%{languageParams.Keyword}%"));
      
      query = languageParams.OrderBy switch
      {
        "code" => query.OrderBy(lit => lit.Code),
        _ => query.OrderBy(lit => lit.Language)
      };

      return await PagedList<LanguageCode>.CreateAsync(
        query, 
        languageParams.PageNumber,
        languageParams.PageSize);
    }

    public LanguageCode AddLanguageCode(LanguageCodeDto code)
    {
      var newCode = _mapper.Map<LanguageCode>(code);
      _context.LanguageCodes.Add(newCode);
      return newCode;
    }

    public void UpdateLanguageCode(LanguageCode code)
    {
      _context.Entry(code).State = EntityState.Modified;
    }

    public void DeleteLanguageCode(LanguageCode code)
    {
      _context.LanguageCodes.Remove(code);
    }    

  }
}
