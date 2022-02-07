using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface ILiteratureRepository
  {
    void AddLiteratureAsync(LiteratureDto literature);
    void DeleteLiterature(Literature literature);
    void UpdateLiterature(Literature literature);
    Task<ICollection<Literature>> GetLiteratureAsync();
    Task<Literature> GetLiteratureAsync(int id);
    Task<ICollection<Literature>> GetLiteratureAsync(string name);
    Task<Literature> GetLiteratureBySymbolAsync(string symbol);
    void AddLanguageCode(LanguageCodeDto code);
    void DeleteLanguageCode(LanguageCode code);
    void UpdateLanguageCode(LanguageCode code);
    Task<ICollection<LanguageCode>> GetLanguageCodesAsync();
    Task<ICollection<LanguageCode>> GetLanguageCodesAsync(string language);
    Task<LanguageCode> GetLanguageCodeAsync(string code);
    Task<bool> SaveAllAsync();
  }
}
