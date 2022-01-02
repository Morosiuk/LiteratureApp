using System;

namespace API.Entities
{
  public class CongregationRole
  {
    public int Id { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public AppUser User { get; set; }
    public int UserId { get; set; }
    public Congregation Congregation { get; set; }
    public int CongregationId { get; set; }
    public Role Role { get; set; }
    public int RoleId { get; set; }

  }
}