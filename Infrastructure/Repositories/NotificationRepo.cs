using Application.Repositories;
using Domain;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNewAsync(Notification instruction)
        {
            await _context.Notifications.AddAsync(instruction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Notification instruction)
        {
            _context.Notifications.Remove(instruction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                .Include(i => i.Instruction)
                .ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notifications
                .Include(i => i.Instruction).ThenInclude(i => i.User)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<SendNotificaitonDto>> GetByInstructionIdAsync(int id)
        {
            var result = await _context.Notifications
                    .Where(n => n.InstructionId == id)
                    .Include(n => n.Instruction)
                        .ThenInclude(i => i.User)
                    .Select(n => new SendNotificaitonDto
                    {
                        Id = n.Id,
                        ChannelType = n.ChannelType,
                        InstructionId = n.Instruction.Id,
                        UserName = n.Instruction.User.Name
                    })
                    .ToListAsync();

            return result;
        }

        public async Task UpdateAsync(Notification instruction)
        {
            _context.Notifications.Update(instruction);
            await _context.SaveChangesAsync();
        }
    }
}
