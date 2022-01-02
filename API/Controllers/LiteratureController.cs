using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class LiteratureController : BaseApiController
  {
    private readonly DataContext _context;
    public LiteratureController(DataContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Literature>>> GetLiteratureAsync()
    {
      return await _context.Literature.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Literature>> GetLiteratureAsync(int Id)
    {
      return await _context.Literature.FindAsync(Id);
    }


    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddLiteratureItem(LiteratureDto literatureDto)
    {
      if (await LiteratureExists(literatureDto)) return BadRequest("Literature already exists.");
      
      var literature = new Literature
      {
        Name = literatureDto.Name,
        FullName = literatureDto.FullName,
        ItemId = literatureDto.ItemId,
        Format = literatureDto.Format,
        Symbol = literatureDto.Symbol
      };

      _context.Literature.Add(literature);
      return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> LiteratureExists(LiteratureDto literatureDto)
    {
      //Check symbol is unique
      var existingSymbol = await _context.Literature
        .AnyAsync(lit => lit.Symbol == literatureDto.Symbol.ToLower());
      if (existingSymbol) return true;

      //Check ItemId is unique (if provided)
      if (literatureDto.ItemId != null) 
      {
        var existingItem = await _context.Literature
          .AnyAsync(lit => lit.ItemId == literatureDto.ItemId);
        if (existingItem) return true;
      }
      return false;
    }
  }
}