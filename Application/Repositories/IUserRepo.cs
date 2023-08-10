using Domain;

namespace Application.Repositories
{
    public interface IUserRepo
    {
        Task AddNewAsync(User user);
        Task DeleteAsync(User user);
        Task<List<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task<User> GetByIdAsync(int id);
    }
}
