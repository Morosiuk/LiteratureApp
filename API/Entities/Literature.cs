using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Literature
  {
   public int Id { get; set; } 
   [Required] public string Name { get; set; }
   public string FullName { get; set; }
   public int? ItemId { get; set; }
   [Required] public string Symbol { get; set; }
   public int? EditionsPerYear { get; set; }
  }
}