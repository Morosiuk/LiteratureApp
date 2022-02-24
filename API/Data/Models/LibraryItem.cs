using System.Globalization;
using API.Entities;

namespace API.Data.Models
{
  public class LibraryItem
  {
    public Literature Literature { get; set; }
    public LanguageCode LanguageCode { get; set; }
    public int? Year { get; set; }
    public int? Number { get; set; }

    public string GetYear()
    {
      if (Year == null || Year.HasValue == false) return "Unknown";

      return CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(Year.Value).ToString();
    }
    
  }
}
