using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
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
    private readonly IUserRepository _userRepo;
    private readonly ICongregationRepository _congRepo;
    private readonly IMapper _mapper;

    public PublishersController(
      IPublisherRepository pubRepo, 
      IUserRepository userRepo, 
      ICongregationRepository congRepo,
      IMapper mapper)
    {
      _pubRepo = pubRepo;
      _userRepo = userRepo;
      _congRepo = congRepo;
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

    [HttpPost("add")]
    public async Task<ActionResult<PublisherDto>> AddPublisherAsync(PublisherDto publisher)
    {
      //Guards
      if(publisher == null) return BadRequest("No publisher received");
      if(string.IsNullOrWhiteSpace(publisher.FirstName) || 
        string.IsNullOrWhiteSpace(publisher.Surname) ||
        string.IsNullOrWhiteSpace(publisher.Congregation))
        return BadRequest("No congregation name provided");

      //Check publisher doesn't already exist
      var searchValue = _mapper.Map<PublisherSearchDto>(publisher);
      var existingPub = await _pubRepo.GetPublisherAsync(searchValue);
      if (existingPub != null) return BadRequest("Publisher already exists");

      //Find congregation to assign publisher too
      var congregation = await _congRepo.GetCongregationAsync(publisher.Congregation);
      if (congregation == null) return BadRequest("Unable to find congregation.");

      //Create new publisher
      var newPublisher = _mapper.Map<Publisher>(publisher);
      newPublisher.Congregation = congregation;
      _pubRepo.AddPublisher(newPublisher);

      var result = await _pubRepo.SaveAllAsync();
      if (result) return Ok(publisher);
      
      return BadRequest("Failed to add congregation");
    }

    [HttpPut]
    public async Task<ActionResult> UpdatePublisher(PublisherUpdateDto updateDto)
    {
      //Get the user putting this request
      var username = HttpContext.User.GetUsername();
      var user = await _userRepo.GetUserAsync(username);
      if (user == null) return BadRequest("Failed to find your user info");

      //Find their publisher entity
      var publishers = await _pubRepo.GetPublishersForUserAsync(user.Id);
      var publisherToUpdate = publishers.FirstOrDefault();
      if (publisherToUpdate != null)
      {
        //Update their publisher info
        _mapper.Map(updateDto, publisherToUpdate);
        _pubRepo.Update(publisherToUpdate);

        if (await _pubRepo.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the publisher.");
      }

      return BadRequest("Failed to find and update the publisher.");
    }
  }
}
