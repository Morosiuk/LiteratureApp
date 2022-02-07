using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class LiteratureController : BaseApiController
  {
    private readonly ILiteratureRepository _litRepo;

    public LiteratureController(ILiteratureRepository litRepo)
    {
      _litRepo = litRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Literature>>> GetLiteratureAsync()
    {
      var results = await _litRepo.GetLiteratureAsync();
      return Ok(results);
    }

    [HttpGet("codes")]
    public async Task<ActionResult<IEnumerable<LanguageCode>>> GetLanguageCodesAsync()
    {
      var results = await _litRepo.GetLanguageCodesAsync();
      return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Literature>> GetLiteratureAsync(int Id)
    {
      if (Id <= 0) return BadRequest("Invalid literature request");

      return await _litRepo.GetLiteratureAsync(Id);
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddLiteratureItem(LiteratureDto literatureDto)
    {
      if (literatureDto == null) return BadRequest("No literature provided");
      if (string.IsNullOrWhiteSpace(literatureDto.Name)) return BadRequest("No literature name provded.");
      
      _litRepo.AddLiteratureAsync(literatureDto);
      var result = await _litRepo.SaveAllAsync();
      if (result) return Ok();

      return BadRequest("Failed to add literature.");
    }

    [HttpPost("codes/add")]
    public async Task<ActionResult<bool>> AddLanguageCode(LanguageCodeDto languageCode)
    {
      if (languageCode == null) return BadRequest("No language code provided");
      if (string.IsNullOrWhiteSpace(languageCode.Language)) return BadRequest("No language provded.");
      
      _litRepo.AddLanguageCode(languageCode);
      var result = await _litRepo.SaveAllAsync();
      if (result) return Ok();

      return BadRequest("Failed to add literature.");
    }
  }
}