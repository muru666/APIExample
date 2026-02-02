using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProbarGiladassss.Data.Models;

namespace ProbarGiladassss.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicoController : ControllerBase
{
    private readonly TestingContext _context;

    public MedicoController(TestingContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var dtos = await _context.Medicos
            .Select(m => new MedicoOutputDto(m.Nombre, m.Apellido, m.EspecialidadNavigation.Nombre))
            .ToListAsync();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var dto = await _context.Medicos
            .Where(m => m.Id == id)
            .Select(d => new MedicoOutputDto(d.Nombre, d.Apellido, d.EspecialidadNavigation.Nombre))
            .FirstOrDefaultAsync();
        
        return dto == null ? NotFound() : Ok(dto);
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicoCreateDto dto)
    {
        Especialidad especialidad = await _context.Especialidads.FindAsync(dto.EspecialidadId);
        if (especialidad is null)
            return NotFound("No existe la especialidad ingresada");


        Medico medico = new Medico()
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Especialidad = dto.EspecialidadId
        };

        await _context.Medicos.AddAsync(medico);
        await _context.SaveChangesAsync();
        return Ok("Creado con éxito");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MedicoCreateDto dto)
    {
        var rows = await _context.Medicos
            .Where(m => m.Id == id)
            .ExecuteUpdateAsync<Medico>(s => s
                .SetProperty(m => m.Nombre, dto.Nombre)
                .SetProperty(m => m.Apellido, dto.Apellido)
                .SetProperty(m => m.Especialidad, dto.EspecialidadId)
            );
        return rows == 0 ? NotFound() : Ok("Actualizado");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var rows = await _context.Medicos
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync();
        return rows == 0 ? NotFound() : Ok("Borrado con éxito");

    }
    public record MedicoCreateDto(string Nombre, string Apellido, int EspecialidadId);

    public record MedicoOutputDto(string Nombre, string Apellido, string Especialidad);


}