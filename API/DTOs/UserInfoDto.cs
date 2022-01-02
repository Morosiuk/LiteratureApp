using System;
using System.Collections.Generic;

namespace API.DTOs
{
  public class UserInfoDto
  {
    public int Id { get; set; }   
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; } 
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public ICollection<CongregationRoleDto> CongregationRoles { get; set; }
    public bool Admin { get; set; }
  }
}