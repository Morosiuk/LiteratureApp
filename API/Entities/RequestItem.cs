namespace API.Entities
{
  public class RequestItem
  {
    public int Id { get; set; }
    public int RequestId { get; set; }
    public Request Request { get; set; }
    public string RequestText { get; set; }
    public int? LiteratureId { get; set; }
    public Literature Literature { get; set; }
    public int? LanguageCodeId { get; set; }
    public LanguageCode LanguageCode { get; set; }
    public int? Edition { get; set; }
    public int? ItemYear { get; set; }
    public string LiteratureCode { get; set; }
  }
}
