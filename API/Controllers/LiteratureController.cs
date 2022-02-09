using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("codes")]
    public async Task<ActionResult<IEnumerable<LanguageCode>>> GetLanguageCodesAsync()
    {
      var results = await _litRepo.GetLanguageCodesAsync();
      return Ok(results);
    }

    [HttpGet("codes/{code}")]
    public async Task<ActionResult<LanguageCode>> GetLanguageCode(string code)
    {
      if (string.IsNullOrWhiteSpace(code)) return BadRequest("Invalid code.");

      return await _litRepo.GetLanguageCodeAsync(code);
    }

    [HttpPost("codes/add")]
    public async Task<ActionResult<bool>> AddLanguageCode(LanguageCodeDto languageCode)
    {
      if (languageCode == null) return BadRequest("No language code provided.");
      if (string.IsNullOrWhiteSpace(languageCode.Language)) return BadRequest("No language provided.");
      if (string.IsNullOrWhiteSpace(languageCode.Code)) return BadRequest("No code provided.");
      if (await _litRepo.GetLanguageCodeAsync(languageCode.Code) != null) return BadRequest("Language code already exists.");

      _litRepo.AddLanguageCode(languageCode);
      var result = await _litRepo.SaveAllAsync();
      if (result) return Ok();

      return BadRequest("Failed to add language code.");
    }

    [HttpPut("codes/update")]
    public async Task<ActionResult> UpdateLanguageCodeAsync(UpdateLanguageCodeDto updatedLanguageCode)
    {
      if (updatedLanguageCode == null) return BadRequest("No language code provided.");
      if (updatedLanguageCode.Id <= 0) return BadRequest("Invalid language code.");
      if (string.IsNullOrWhiteSpace(updatedLanguageCode.Language)) return BadRequest("No language provided.");
      //Lookup existing code
      var languageCode = await _litRepo.GetLanguageCodeAsync(updatedLanguageCode.Id);
      if (languageCode == null) return BadRequest("Unable to find relevant language code.");
      //Update values
      languageCode.Language = updatedLanguageCode.Language;
      languageCode.Code = updatedLanguageCode.Code;
      _litRepo.UpdateLanguageCode(languageCode);

      var result = await _litRepo.SaveAllAsync();
      if (result) return Ok("Language code successfully updated.");

      return BadRequest("Failed to update language code.");
    }

    [HttpDelete("codes/delete/{code}")]
    public async Task<ActionResult> DeleteLanguageCode(string code)
    {
      if (code == null) return BadRequest("No language code provided.");
      if (string.IsNullOrWhiteSpace(code)) return BadRequest("No language code provided.");

      var languageCode = await _litRepo.GetLanguageCodeAsync(code);
      if (languageCode == null) return NotFound("Cannot find language code.");

      _litRepo.DeleteLanguageCode(languageCode);
      var result = await _litRepo.SaveAllAsync();
      if (result) return Ok();

      return BadRequest("Failed to delete language code.");
    }
  }
}