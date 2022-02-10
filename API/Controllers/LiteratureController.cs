using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers.Params;
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
    public async Task<ActionResult<IEnumerable<Literature>>> GetLiteratureAsync([FromQuery]LiteratureParams litParams)
    {
      var results = await _litRepo.GetLiteratureAsync(litParams);
      Response.AddPaginationHeader(results.CurrentPage, results.PageSize, results.TotalCount, results.TotalPages);

      return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Literature>> GetLiteratureAsync(int Id)
    {
      if (Id <= 0) return BadRequest("Invalid literature request");

      return await _litRepo.GetLiteratureAsync(Id);
    }

    [HttpGet("{symbol}")]
    public async Task<ActionResult<Literature>> GetLiteratureAsync(string symbol)
    {
      if (symbol == null || string.IsNullOrWhiteSpace(symbol)) 
        return BadRequest("No lookup value provided.");

      return await _litRepo.GetLiteratureAsync(symbol);
    }

    [HttpGet("item/{itemId}")]
    public async Task<ActionResult<Literature>> GetLiteratureFromItemAsync(int itemId)
    {
      if (itemId <= 0) return BadRequest("No Item ID provided.");
      return await _litRepo.GetLiteratureFromItemAsync(itemId);
    }

    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddLiterature(LiteratureDto literatureDto)
    {
      if (literatureDto == null) return BadRequest("No literature provided");
      if (string.IsNullOrWhiteSpace(literatureDto.Name)) 
        return BadRequest("No literature name provded.");
      
      if (await _litRepo.GetLiteratureAsync(literatureDto.Symbol) != null)
        return BadRequest("Literature with this symbol already exists.");

      var literature = _litRepo.AddLiterature(literatureDto);
      var result = await _litRepo.SaveAllAsync();
      if (result) return Ok(literature);

      return BadRequest("Failed to add literature.");
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateLiteratureAsync(UpdateLiteratureDto updatedLiterature)
    {
      if (updatedLiterature == null) return BadRequest("No literature provided.");
      if (updatedLiterature.Id <= 0) return BadRequest("Invalid literature.");
      if (string.IsNullOrWhiteSpace(updatedLiterature.Symbol)) return BadRequest("No literature symbol.");
      if (string.IsNullOrWhiteSpace(updatedLiterature.Name)) return BadRequest("No literature name.");
      //Lookup existing code
      var literature = await _litRepo.GetLiteratureAsync(updatedLiterature.Id);
      if (literature == null) return BadRequest("Unable to find literature.");
      //Update values
      literature.Name = updatedLiterature.Name;
      literature.FullName = updatedLiterature.FullName;
      literature.Symbol = updatedLiterature.Symbol;
      literature.EditionsPerYear = updatedLiterature.EditionsPerYear;
      literature.ItemId = updatedLiterature.ItemId;
      _litRepo.UpdateLiterature(literature);

      var result = await _litRepo.SaveAllAsync();
      if (result) return Ok("Literature successfully updated.");

      return BadRequest("Failed to update literature.");
    }

    [HttpDelete("delete/{symbol}")]
    public async Task<ActionResult> DeleteLiterature(string symbol)
    {
      if (symbol == null) return BadRequest("No literature provided.");
      if (string.IsNullOrWhiteSpace(symbol)) return BadRequest("No literature symbol provided.");

      var literature = await _litRepo.GetLiteratureAsync(symbol);
      if (literature == null) return NotFound("Cannot find literature.");

      _litRepo.DeleteLiterature(literature);
      var result = await _litRepo.SaveAllAsync();
      if (result) return Ok();

      return BadRequest("Failed to delete literature.");
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

      var createdCode = _litRepo.AddLanguageCode(languageCode);
      var result = await _litRepo.SaveAllAsync();
      if (result) return Ok(createdCode);

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