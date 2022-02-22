using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class RequestController : BaseApiController
  {
    private readonly IRequestRepository _requestRepo;
    private readonly IMapper _mapper;

    public RequestController(
      IRequestRepository requestRepo, 
      IMapper mapper)
    {
      _requestRepo = requestRepo;
      _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddRequest(AddRequestDto requestDto)
    {
      if (requestDto == null) return BadRequest("No request provided");
      if (requestDto.Items == null || !requestDto.Items.Any()) 
        return BadRequest("No items requested");
      
      var request = _requestRepo.AddRequest(requestDto);  
      foreach (var item in requestDto.Items)
      {
        await _requestRepo.AddRequestItemAsync(item);
      }

      var result = await _requestRepo.SaveAllAsync();
      if (result) return Ok(request);

      return BadRequest("Failed to add request.");
    }
  }
}
