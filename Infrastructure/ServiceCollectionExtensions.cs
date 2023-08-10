using Application.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddTransient<IUserRepo, UserRepo>()
                .AddTransient<IInstructionRepo, InstructionRepo>()
                .AddTransient<INotificationRepo, NotificationRepo>()
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlite(configuration.GetConnectionString("Sqlite")));
        }
    }
}
