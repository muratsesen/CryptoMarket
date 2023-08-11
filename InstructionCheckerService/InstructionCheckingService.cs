using Infrastructure.Repositories;
using Application.Repositories;
using Domain;
public class InstructionCheckingService
{
    IServiceProvider _serviceProvider;
    public InstructionCheckingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task Execute()
    {
        var repo = _serviceProvider.GetService<IInstructionRepo>();
   
        if (repo != null)
        {
            DateTime today = DateTime.Today;
             int dayOfMonth = today.Day;

            List<Instruction> instructions = await repo.GetAllAsync();
            if (instructions != null)
            {
                foreach (var instruction in instructions)
                {
                    if(instruction.DayOfMonth == dayOfMonth)
                    {
                        System.Console.WriteLine("Bu talimat uygulanmalı"+instruction.Id);
                        //Send message broker
                    }
                     else    System.Console.WriteLine("(-)Bu talimat uygulanmamalı"+instruction.Id);

                }
            }
        }
    }
}