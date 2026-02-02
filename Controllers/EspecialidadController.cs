using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProbarGiladassss.Data.Models;

namespace ProbarGiladassss.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EspecialidadController : ControllerBase
{
    private readonly TestingContext _context;
    public EspecialidadController(TestingContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var dtos = await _context.Especialidads
            .Select(e => new EspecialidadDto(e.Nombre))
            .ToListAsync();
        return Ok(dtos);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var dto = await _context.Especialidads
            .Where(e => e.Id == id)
            .Select(e => new EspecialidadDto(e.Nombre))
            .FirstOrDefaultAsync();

        return Ok(dto);
    }

    [HttpPost]
    public async Task<bool> Create(EspecialidadDto dto)
    {
        Especialidad especialidadNueva = new Especialidad()
        {
            Nombre = dto.Nombre
        };
        await _context.Especialidads.AddAsync(especialidadNueva);
        return await _context.SaveChangesAsync() > 0;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EspecialidadDto dto)
    {
        var encontrado = await _context.Especialidads.FindAsync(id);
        if (encontrado is null)
            return NotFound();
        encontrado.Nombre = dto.Nombre;
        await _context.SaveChangesAsync();
        return Ok("Un éxito");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var encontrado = await _context.Especialidads.FindAsync(id);
        if (encontrado is null)
            return NotFound();
        _context.Especialidads.Remove(encontrado);
        return Ok(await _context.SaveChangesAsync());
    }

    public record EspecialidadDto(string Nombre);


}