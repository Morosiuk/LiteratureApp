using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Helpers.Params;

namespace API.Interfaces
{
  public interface ILiteratureRepository
  {
    Literature AddLiterature(LiteratureDto literature);
    void DeleteLiterature(Literature literature);
    void UpdateLiterature(Literature literature);
    Task<PagedList<Literature>> GetLiteratureAsync(LiteratureParams litParams);
    Task<Literature> GetLiteratureAsync(int id);
    Task<Literature> GetLiteratureAsync(string symbol);
    Task<Literature> GetLiteratureFromItemAsync(int itemId);
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
