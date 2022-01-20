using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace API.Entities
{
  public class AppUser
  {
    public int Id { get; set; }
    [Required] public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastActive { get; set; } = DateTime.Now;
    public ICollection<AppUserPublisher> AssignedPublishers { get; set; } 
      = new List<AppUserPublisher>();
    public bool Admin { get; set; }

    internal bool AssignPublisher(Publisher publisher)
    {
      if (publisher == null) return false;
      //Check they don't already have a publisher for this congregation
      if (AssignedPublishers.Any(p => 
        p.Publisher.CongregationId == publisher.CongregationId)) return false;

      AssignedPublishers.Add(new AppUserPublisher{
        User = this,
        Publisher = publisher
      });
      return true;
    }
  }
}