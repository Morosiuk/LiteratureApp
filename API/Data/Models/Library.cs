using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using API.Entities;

namespace API.Data.Models
{
  public class Library
  {
    public ICollection<Literature> Literature { get; set; }
    public Dictionary<string, LanguageCode> LanguageCodes { get; set; }

    public Library()
    {
      Literature = new List<Literature>();
      LanguageCodes = new Dictionary<string, LanguageCode>();
    }

    public void SetLanguageCodes(ICollection<LanguageCode> languageCodes)
    {
      if (languageCodes == null || !languageCodes.Any()) return;

      this.LanguageCodes = languageCodes
        .GroupBy(lc => lc.Code)
        .ToDictionary(keySelector: lc => lc.Key, elementSelector: lc => lc.FirstOrDefault()); 
    }

    public LibraryItem FindLibraryItem(RequestItem requestItem)
    {
      if (requestItem == null) return null;

      //Use the symbol code provided in the request
      if (!string.IsNullOrWhiteSpace(requestItem.Symbol))
        return LookupCode(requestItem.Symbol);

      return null;
    }
  

    public LibraryItem LookupCode(string code)
    {
      if (string.IsNullOrWhiteSpace(code)) return null;
      var LibraryItem = new LibraryItem();

      //Regex to extract <LiteratureCode><Edition>-<LanguageCode>
      string pattern = @"([^-]*)$";  
      // Create a Regex  
      Regex rg = new Regex(pattern); 
      Match match = rg.Match(code);
      if (match.Success)
      {
        var languageCode = match.Value;
        if (LanguageCodes.ContainsKey(languageCode)) LibraryItem.LanguageCode = LanguageCodes[languageCode];
      }

      return LibraryItem;
    }

  }
}
