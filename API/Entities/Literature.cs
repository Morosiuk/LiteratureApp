namespace API.Entities
{
  public class Literature
  {
   public int Id { get; set; } 
   public string Name { get; set; }
   public string FullName { get; set; }
   public int? ItemId { get; set; }
   public string Symbol { get; set; }
   public string Language { get; set; }
   public string Format { get; set; }
  }
}