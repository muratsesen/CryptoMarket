using Domain;

namespace Application.Repositories
{
    public interface IInstructionRepo
    {
        Task<Instruction> AddNewAsync(Instruction instruction);
        Task DeleteAsync(Instruction instruction);
        Task<List<Instruction>> GetAllAsync();
        Task UpdateAsync(Instruction instruction);
        Task<Instruction> GetByIdAsync(int id);
    }
}
