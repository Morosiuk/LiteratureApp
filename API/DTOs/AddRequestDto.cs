using System.Collections.Generic;

namespace API.DTOs
{
  public class AddRequestDto
  {
    public int PublisherId { get; set; }
    public string Information { get; set; }
    public ICollection<AddRequestItemDto> Items { get; set; }
  }
}
