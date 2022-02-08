using System;

namespace API.DTOs
{
  public class UpdateLanguageCodeDto
  {
    public int Id { get; set; }
    public string Language { get; set; }
    public string Code { get; set; }
  }
}
