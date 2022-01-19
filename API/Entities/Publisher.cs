using System;

namespace API.Entities
{
  public class Publisher
  {
    public int Id { get; set; }
    public String Firstname { get; set; }
    public String Surname { get; set; }
    public DateTime DateCreated { get; set; }
    public int CongregationId { get; set; }
    public Congregation Congregation { get; set; }
    public int? RoleId { get; set; }
    public Role Role { get; set; }

  }
}
