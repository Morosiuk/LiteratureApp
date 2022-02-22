namespace API.DTOs
{
  public class AddRequestItemDto
  {
    public int RequestId { get; set; }
    public string RequestText { get; set; }
    public int? LiteratureId { get; set; }
    public int? LanguageCodeId { get; set; }
    public int? Edition { get; set; }
    public int? ItemYear { get; set; }
    public string LiteratureCode { get; set; }
    public string Symbol { get; set; }
  }
}
