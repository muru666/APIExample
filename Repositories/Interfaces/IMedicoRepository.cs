using ProbarGiladassss.DTOs;

namespace ProbarGiladassss.Repositories.Interfaces;

public interface IMedicoRepository
{
    Task<List<MedicoOutputDto>> GetAllMedicosAsync();
    Task<MedicoOutputDto> GetMedicoByIdAsync(int id);
    Task<bool> CreateMedicoAsync(MedicoCreateDto dto);
    Task<bool> UpdateMedicoAsync(int id, MedicoCreateDto dto);
    Task<bool> DeleteMedicoAsync(int id);
}