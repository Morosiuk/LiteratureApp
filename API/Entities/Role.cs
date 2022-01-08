using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Role
  {
    public int Id { get; set; }
    [Required] public string Name { get; set; }
    public string Description { get; set; }
    public bool Admin { get; set; }
    public bool Default { get; set; }
  }
}