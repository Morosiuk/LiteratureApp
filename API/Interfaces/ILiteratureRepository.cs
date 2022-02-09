using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface ILiteratureRepository
  {
    Literature AddLiterature(LiteratureDto literature);
    void DeleteLiterature(Literature literature);
    void UpdateLiterature(Literature literature);
    Task<ICollection<Literature>> GetLiteratureAsync();
    Task<Literature> GetLiteratureAsync(int id);
    Task<Literature> GetLiteratureBySymbolAsync(string symbol);
    Task<Literature> GetLiteratureByItemAsync(int itemId);
    Task<ICollection<Literature>> GetLiteratureAsync(string name);
    LanguageCode AddLanguageCode(LanguageCodeDto code);
    void DeleteLanguageCode(LanguageCode code);
    void UpdateLanguageCode(LanguageCode code);
    Task<ICollection<LanguageCode>> GetLanguageCodesAsync();
    Task<ICollection<LanguageCode>> GetLanguageCodesAsync(string language);
    Task<LanguageCode> GetLanguageCodeAsync(int id);
    Task<LanguageCode> GetLanguageCodeAsync(string code);
    Task<bool> SaveAllAsync();
  }
}
