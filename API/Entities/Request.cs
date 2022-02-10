using System;
using System.Collections.Generic;

namespace API.Entities
{
  public class Request
  {
    public int Id { get; set; }
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    public string Information { get; set; }
    public DateTime RequestDate { get; set; }
    public ICollection<RequestItem> Items { get; set; }

  }
}
