using ProbarGiladassss.DTOs;
using ProbarGiladassss.Repositories.Interfaces;

namespace ProbarGiladassss.Repositories;

public class MedicoRepository: IMedicoRepository
{
    public Task<List<MedicoOutputDto>> GetAllMedicosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<MedicoOutputDto> GetMedicoByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateMedicoAsync(MedicoCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateMedicoAsync(int id, MedicoCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteMedicoAsync(int id)
    {
        throw new NotImplementedException();
    }
}