using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Publisher
  {
    public int Id { get; set; }
    [Required] public String Firstname { get; set; }
    [Required] public String Surname { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public int CongregationId { get; set; }
    public Congregation Congregation { get; set; }
    public int? RoleId { get; set; }
    public Role Role { get; set; }

  }
}
