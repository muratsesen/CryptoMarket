using Domain;

namespace Application.Repositories
{
    public interface INotificationRepo
    {
        Task AddNewAsync(Notification instruction);
        Task DeleteAsync(Notification instruction);
        Task<List<Notification>> GetAllAsync();
        Task UpdateAsync(Notification instruction);
        Task<Notification> GetByIdAsync(int id);
        Task<List<SendNotificaitonDto>> GetByInstructionIdAsync(int id);
    }
}
