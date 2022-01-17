using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using API.DTOs;
using System;
using API.Helpers;
using API.Extensions;

namespace API.Controllers
{
  [Authorize]
  public class CongregationsController : BaseApiController
  {
    private readonly ICongregationRepository _congregationRepo;
    private readonly IMapper _mapper;

    public CongregationsController(ICongregationRepository congregationRepo, IMapper mapper)
    {
      _mapper = mapper;
      _congregationRepo = congregationRepo;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CongregationDto>>> GetCongregationsAsync([FromQuery]CongParams congParams)
    {
      var congregations = await _congregationRepo.GetCongregationsAsync(congParams);
      Response.AddPaginationHeader(congregations.CurrentPage, 
        congregations.PageSize, congregations.TotalCount, congregations.TotalPages);

      return Ok(congregations);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Congregation>> GetCongregationAsync(int Id)
    {
      return await _congregationRepo.GetCongregationAsync(Id);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Congregation>> GetCongregationAsync(string name)
    {
      return await _congregationRepo.GetCongregationAsync(name);
    }

    [HttpPost("add")]
    public async Task<ActionResult<Congregation>> AddCongregationAsync(AddCongregationDto congregation)
    {
      if(congregation == null) return BadRequest("No congregation passed");
      if(congregation.Name == null || congregation.Name.Length == 0) 
        return BadRequest("No congregation name provided");

      //Check congregation doesn't already exist
      var existingCong = await _congregationRepo.GetCongregationAsync(congregation.Name);
      if (existingCong != null) return BadRequest("Congregation already exists");

      //Create new congregation
      var newCongregation = _mapper.Map<Congregation>(congregation);
      newCongregation.DateCreated = DateTime.Now;
      _congregationRepo.AddCongregation(newCongregation);

      var result = await _congregationRepo.SaveAllAsync();
      if (result) return Ok(newCongregation);
      
      return BadRequest("Failed to add congregation");
    }
  }
}