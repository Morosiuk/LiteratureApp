using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Models;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
  public class RequestRepository : IRequestRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private Library _library;

    public RequestRepository(DataContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public Request AddRequest(AddRequestDto request)
    {
      var newRequest = _mapper.Map<Request>(request);
      _context.Requests.Add(newRequest);
      return newRequest;
    }

    public async Task<RequestItem> AddRequestItemAsync(AddRequestItemDto requestItem)
    {
      var newRequestItem = _mapper.Map<RequestItem>(requestItem);

      //Lookup the request item in the library
      if (_library == null) _library = await PrepareLibraryAsync();
      var item = _library.FindLibraryItem(newRequestItem);
      if (item == null) return null;

      newRequestItem.LanguageCode = item.LanguageCode;
      newRequestItem.Literature = item.Literature;
      //Add request
      _context.RequestItems.Add(newRequestItem);
      return newRequestItem;
    }

    public void DeleteRequest(Request request)
    {
      throw new NotImplementedException();
    }

    public void DeletRequestItem(RequestItem requestItem)
    {
      throw new NotImplementedException();
    }

    public Task<Request> GetRequestAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<Request> GetRequestItemAsyc(int id)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<RequestItem>> GetRequestItems()
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Request>> GetRequestsAsync()
    {
      throw new NotImplementedException();
    }

    public Task<bool> SaveAllAsync()
    {
      throw new NotImplementedException();
    }

    public void UpdateRequest(Request request)
    {
      throw new NotImplementedException();
    }

    public void UpdateRequest(RequestItem requestItem)
    {
      throw new NotImplementedException();
    }

    private async Task<Library> PrepareLibraryAsync()
    {
      var languageCodes = await _context.LanguageCodes.ToListAsync();
      var library = new Library();
      library.SetLanguageCodes(languageCodes);
      return library;
    }
  }
}
