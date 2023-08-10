
using Infrastructure.Repositories;

namespace WebApi.HostedServices
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly InstructionRepo instractionRepo;
        // private readonly IMySingletonService _mySingletonService;
        // private readonly IMyTransientService _myTransientService;
        private readonly IServiceProvider _serviceProvider;

        public MyBackgroundService(InstructionRepo _instructionRepo, IServiceProvider serviceProvider)
        {
            instractionRepo = _instructionRepo;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var instructionRepo = scope.ServiceProvider.GetRequiredService<InstructionRepo>();

                    var ins = await instructionRepo.GetAllAsync();
                    if (ins.Count > 0)
                        foreach (var item in ins)
                            Console.WriteLine(string.Format("{0} - {1}", item.Id, item.Amount));

                    else
                        Console.WriteLine("No data");
                        
                    Console.WriteLine(string.Format("{0} - {1}", "MyBackgroundService", DateTime.UtcNow.ToString("HH:mm:ss")));
                    await Task.Delay(new TimeSpan(0, 0, 1));
                }
            }
        }
    }
}
