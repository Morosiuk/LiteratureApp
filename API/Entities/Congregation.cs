using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
  public class Congregation
  {
    public int Id { get; set; }
    [Required] public string Name { get; set; }
    public int? Code { get; set; }
    public DateTime DateCreated { get; set; }
    public ICollection<Publisher> Publishers { get; set; }
  }
}