using System;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
  public interface IPublisherRepository
  {
    void Update(Publisher publisher);
    Task<bool> SaveAllAsync();
    Task<PublisherDto> GetPublisherAsync(int id);
    //Task<PublisherDto> GetPublisherAsync(string name, string congregation);
    Task<PagedList<PublisherDto>> GetPublishersAsync(PublisherParams pubParams);
    
  }
}
