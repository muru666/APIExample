using ProbarGiladassss.Data.Models;
using ProbarGiladassss.DTOs;

namespace ProbarGiladassss.Repositories.Interfaces;

public interface IEspecialidadRepository
{
    Task<List<EspecialidadDto>> GetAllEspecialidadesAsync();
    Task<EspecialidadDto> GetEspecialidadByIdAsync(int id);
    Task<Especialidad> CreateEspecialidadAsync(EspecialidadDto dto);
    Task<bool> UpdateEspecialidadAsync(int id, EspecialidadDto dto);
    Task<bool> DeleteEspecialidadAsync(int id);
}