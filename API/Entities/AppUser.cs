using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }   
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }  
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public ICollection<CongregationRole> CongregationRoles { get; set; }
        public bool Admin { get; set; }

    internal void AddRole(Role role, int congregation)
    {
      if (CongregationRoles == null) {CongregationRoles = new List<CongregationRole>();}
      if (CongregationRoles.Any(cr => 
        cr.CongregationId == congregation &&
        cr.RoleId == role.Id)) return;

      CongregationRoles.Add(new CongregationRole {
        CongregationId = congregation,
        RoleId = role.Id,
        UserId = this.Id,
        StartDate = DateTime.Now
      });
    }
  }
}