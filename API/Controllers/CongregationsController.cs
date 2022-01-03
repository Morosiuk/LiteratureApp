using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.Entities;
using API.Interfaces;

namespace API.Controllers
{
  [Authorize]
  public class CongregationsController : BaseApiController
  {
    private readonly ICongregationRepository _congregationRepo;

    public CongregationsController(ICongregationRepository congregationRepo)
    {
      _congregationRepo = congregationRepo;
    }

    [HttpGet]
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
  }
}