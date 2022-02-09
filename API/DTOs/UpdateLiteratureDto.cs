namespace API.DTOs
{
  public class UpdateLiteratureDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public int? ItemId { get; set; }
    public string Symbol { get; set; }
    public int? EditionsPerYear { get; set; }
  }
}
