using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class LanguageCode
  {
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    public string Language { get; set; }
  }
}
