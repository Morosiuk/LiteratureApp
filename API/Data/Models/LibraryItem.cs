using System;
using API.Entities;

namespace API.Data.Models
{
  public class LibraryItem
  {
    public Literature Literature { get; set; }
    public LanguageCode LanguageCode { get; set; }
    
  }
}
