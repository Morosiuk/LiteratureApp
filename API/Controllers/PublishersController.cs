using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class PublishersController : BaseApiController
  {
    private readonly IPublisherRepository _pubRepo;
    private readonly IMapper _mapper;

    public PublishersController(IPublisherRepository pubRepo, IMapper mapper)
    {
      _pubRepo = pubRepo;
      _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PublisherDto>> GetPublisherAsync(int Id)
    {
      return await _pubRepo.GetPublisherAsync(Id);
    }

    [HttpGet]
    public async Task<ActionResult<PublisherDto>> GetPublishersAsync([FromQuery]PublisherParams pubParams)
    {
      var publishers =  await _pubRepo.GetPublishersAsync(pubParams);
      Response.AddPaginationHeader(publishers.CurrentPage, 
        publishers.PageSize, publishers.TotalCount, publishers.TotalPages);

      return Ok(publishers);
    }
  }
}
