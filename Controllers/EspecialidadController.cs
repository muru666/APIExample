using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProbarGiladassss.Data.Models;
using ProbarGiladassss.DTOs;
using ProbarGiladassss.Repositories.Interfaces;

namespace ProbarGiladassss.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EspecialidadController : ControllerBase
{
    private readonly IEspecialidadRepository _context;

    public EspecialidadController(IEspecialidadRepository context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var dtos = await _context.GetAllEspecialidadesAsync();
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var dto = await _context.GetEspecialidadByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(EspecialidadDto dto)
    {
        try
        {
            var especialidad = await _context.CreateEspecialidadAsync(dto);
            var resultDto = new EspecialidadDto(especialidad.Nombre);

            return CreatedAtAction(
                nameof(GetById),
                new { id = especialidad.Id },
                resultDto);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EspecialidadDto dto)
    {
        var updated = await _context.UpdateEspecialidadAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await _context.DeleteEspecialidadAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}