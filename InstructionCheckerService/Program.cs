using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Storage;
using HangfireBasicAuthenticationFilter;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;

IConfiguration _configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
System.Console.WriteLine(_configuration.GetSection("ConnectionStrings:Sqlite").Value);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlite(_configuration.GetSection("ConnectionStrings:Sqlite").Value));


builder.Services.AddTransient<IInstructionRepo, InstructionRepo>();

builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHangfireDashboard("/job", new DashboardOptions
{
    Authorization = new[]
{
    new HangfireCustomBasicAuthenticationFilter
    {
         User = _configuration.GetSection("HangfireSettings:UserName").Value,
         Pass = _configuration.GetSection("HangfireSettings:Password").Value
    }
    }
});

app.UseHangfireServer(new BackgroundJobServerOptions());
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3 });

RecurringJob.AddOrUpdate<InstructionCheckingService>(x => x.Execute(), Cron.MinuteInterval(1));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
