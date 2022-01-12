using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using API.DTOs;

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
    public async Task<ActionResult<IEnumerable<Congregation>>> GetCongregationsAsync()
    {
      var congregations = await _congregationRepo.GetCongregationsAsync();
      return Ok(congregations);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Congregation>> GetCongregationAsync(int Id)
    {
      return await _congregationRepo.GetCongregationAsync(Id);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<Congregation>> GetCongregationAsync(string name)
    {
      return await _congregationRepo.GetCongregationAsync(name);
    }

    [HttpPost]
    public async Task<ActionResult<Congregation>> AddCongregationAsync(AddCongregationDto congregation)
    {
      //Check congregation doesn't already exist
      var existingCong = await _congregationRepo.GetCongregationAsync(congregation.Name);
      if (existingCong != null) return BadRequest("Congregation already exists");

      //Create new congregation
      var newCongregation = _mapper.Map<Congregation>(congregation);
      _congregationRepo.AddCongregation(newCongregation);
      var result = await _congregationRepo.SaveAllAsync();
      if (result) return Ok(newCongregation);

      return BadRequest("Failed to add congregation");
    }
  }
}