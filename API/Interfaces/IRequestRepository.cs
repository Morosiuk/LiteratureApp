using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
  public interface IRequestRepository
  {
    Request AddRequest(AddRequestDto request);
    void DeleteRequest(Request request);
    void UpdateRequest(Request request);
    Task<IEnumerable<Request>> GetRequestsAsync();
    Task<Request> GetRequestAsync(int id);

    Task<RequestItem> AddRequestItemAsync(AddRequestItemDto requestItem);
    void DeletRequestItem(RequestItem requestItem);
    void UpdateRequest(RequestItem requestItem);
    Task<IEnumerable<RequestItem>> GetRequestItems();
    Task<Request> GetRequestItemAsyc(int id);

    Task<bool> SaveAllAsync();
  }
}
