using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Congregation
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public int? Code { get; set; }
  }
}