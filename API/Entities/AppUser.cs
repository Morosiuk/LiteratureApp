using System;
using System.Collections.Generic;

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

    }
}