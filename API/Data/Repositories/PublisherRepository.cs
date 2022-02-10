using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Helpers.Params;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
  public class PublisherRepository : IPublisherRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PublisherRepository(DataContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<PublisherDto> GetPublisherAsync(int id)
    {
      var publisher = await _context.Publishers
        .Include(pub => pub.Congregation)
        .FirstOrDefaultAsync(pub => pub.Id == id);
      return _mapper.Map<PublisherDto>(publisher);
    }

    public async Task<PublisherDto> GetPublisherAsync(PublisherSearchDto publisher)
    {
      var value = await _context.Publishers
        .FirstOrDefaultAsync(pub => 
          pub.Congregation.Name.ToLower() == publisher.Congregation.ToLower() &&
          pub.FirstName.ToLower() == publisher.Firstname.ToLower() &&
          pub.Surname.ToLower() == publisher.Surname.ToLower());
      return _mapper.Map<PublisherDto>(value);
    }

    public async Task<PagedList<PublisherDto>> GetPublishersAsync(PublisherParams pubParams)
    {
      var query = _context.Publishers
        .AsNoTracking()
        .AsQueryable();
      
      var congregation = pubParams.Congregation;
      if (congregation?.Length > 0)
      {
        query = query
          .Where(pub => pub.Congregation.Name.ToLower() == congregation.ToLower());
      }
      query = pubParams.OrderBy switch
      {
        "firstname" => query.OrderBy(pub => pub.FirstName),
        "surname" => query.OrderBy(pub => pub.Surname), 
        _ => query.OrderBy(pub => pub.Id)
      };
      return await PagedList<PublisherDto>.CreateAsync(
        query.ProjectTo<PublisherDto>(_mapper.ConfigurationProvider),
        pubParams.PageNumber,
        pubParams.PageSize);
    }

    public async Task<ICollection<Publisher>> GetPublishersForUserAsync(int userId)
    {
      if (userId <= 0) return null;

      return (await _context.Users
        .Include(u => u.AssignedPublishers)
        .ThenInclude(ap => ap.Publisher)
        .FirstOrDefaultAsync(u => u.Id == userId)).AssignedPublishers
          .Select(ap => ap.Publisher)
          .ToList();
    }

    public void AddPublisher(Publisher publisher)
    {
      _context.Publishers.Add(publisher);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(Publisher publisher)
    {
      _context.Entry(publisher).State = EntityState.Modified;
    }
  }
}
