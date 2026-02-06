using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProbarGiladassss.Data.Models;
using ProbarGiladassss.DTOs;
using ProbarGiladassss.Repositories.Interfaces;

namespace ProbarGiladassss.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicoController : ControllerBase
{
    private readonly IMedicoRepository _context;
    private readonly IEspecialidadRepository _especialidadContext;

    public MedicoController(IMedicoRepository context, IEspecialidadRepository especialidadContext)
    {
        _context = context;
        _especialidadContext = especialidadContext;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var dtos = await _context.GetAllMedicosAsync();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var dto = await _context.GetMedicoByIdAsync(id);
        return dto == null ? NotFound() : Ok(dto);
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicoCreateDto dto)
    {

        var especialidad = await _especialidadContext.GetEspecialidadByIdAsync(dto.EspecialidadId);
        if (especialidad is null)
            return BadRequest("La especialidad no existe.");
        
        var medico = await _context.CreateMedicoAsync(dto);
        var outputDto = new MedicoOutputDto(
            dto.Nombre, 
            dto.Apellido, 
            especialidad.Nombre);
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = medico.Id },
            outputDto);


        /*
         *var especialidad = await _context.CreateEspecialidadAsync(dto);
           var resultDto = new EspecialidadDto(especialidad.Nombre);

           return CreatedAtAction(
               nameof(GetById),
               new { id = especialidad.Id },
               resultDto);
         *
         */
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MedicoCreateDto dto)
    {
        var updated = await _context.UpdateMedicoAsync(id, dto);
        return updated ? NotFound() : NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var deleted = await _context.DeleteMedicoAsync(id);
        return deleted ? NoContent() : NotFound();

    }
   


}