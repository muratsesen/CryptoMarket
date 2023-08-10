using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {            
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Instruction> Instructions => Set<Instruction>();
        public DbSet<Notification> Notifications => Set<Notification>();
    }
}
