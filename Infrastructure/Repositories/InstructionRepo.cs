using Application.Repositories;
using Domain;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InstructionRepo : IInstructionRepo
    {
        private readonly ApplicationDbContext _context;

        public InstructionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Instruction> AddNewAsync(Instruction instruction)
        {
            await _context.Instructions.AddAsync(instruction);
            await _context.SaveChangesAsync();
            return instruction;
        }

        public async Task DeleteAsync(Instruction instruction)
        {
            _context.Instructions.Remove(instruction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Instruction>> GetAllAsync()
        {
            return await _context.Instructions
                .Include(i => i.User)
                .ToListAsync();
        }

        public async Task<Instruction> GetByIdAsync(int id)
        {
            return await _context.Instructions
                .Include(i => i.User)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Instruction instruction)
        {
            _context.Instructions.Update(instruction);
            await _context.SaveChangesAsync();
        }
    }
}
