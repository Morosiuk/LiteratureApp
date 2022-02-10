using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Helpers.Params;

namespace API.Interfaces
{
  public interface IPublisherRepository
  {
    void Update(Publisher publisher);
    void AddPublisher(Publisher publisher);
    Task<bool> SaveAllAsync();
    Task<PublisherDto> GetPublisherAsync(int id);
    Task<ICollection<Publisher>> GetPublishersForUserAsync(int userId);
    Task<PagedList<PublisherDto>> GetPublishersAsync(PublisherParams pubParams);
    Task<PublisherDto> GetPublisherAsync(PublisherSearchDto publisher);
  }
}
