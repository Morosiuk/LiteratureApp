using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using API.Entities;

namespace API.Data.Models
{
  public class Library
  {
    public Dictionary<string, Literature> Literature { get; set; }
    public Dictionary<string, LanguageCode> LanguageCodes { get; set; }

    public Library()
    {
      Literature = new Dictionary<string, Literature>();
      LanguageCodes = new Dictionary<string, LanguageCode>();
    }

    public bool AddLiterature(Literature literature)
    {
      if (literature == null || string.IsNullOrWhiteSpace(literature.Symbol)) return false;

      if (Literature.ContainsKey(literature.Symbol)) return false;
      Literature.Add(literature.Symbol, literature);
      return true;
    }

    public bool AddLanguageCode(LanguageCode languageCode)
    {
      if (languageCode == null || string.IsNullOrWhiteSpace(languageCode.Code)) return false;

      if (LanguageCodes.ContainsKey(languageCode.Code)) return false;
      LanguageCodes.Add(languageCode.Code, languageCode);
      return true;
    }

    public void SetLanguageCodes(ICollection<LanguageCode> languageCodes)
    {
      if (languageCodes == null || !languageCodes.Any()) return;

      LanguageCodes = languageCodes
        .GroupBy(lc => lc.Code)
        .ToDictionary(keySelector: lc => lc.Key, elementSelector: lc => lc.FirstOrDefault()); 
    }

    public void SetLiterature(ICollection<Literature> literature)
    {
      if (literature == null || !literature.Any()) return;

      Literature = literature
        .GroupBy(lit => lit.Symbol)
        .ToDictionary(keySelector: lit => lit.Key, elementSelector: lit => lit.FirstOrDefault()); 
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
