using Infrastructure;
using Application;
using WebApi.Middlewares;
using WebApi;
using WebApi.HostedServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var cacheSettings = builder.Services.GetCacheSettings(builder.Configuration);

//Configure Redis

// builder.Services.AddStackExchangeRedisCache(options =>
// {
//     options.Configuration = cacheSettings.DistinationUrl;
// });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 //builder.Services.AddHostedService<MyBackgroundService>();
// builder.Services.AddHostedService<MyHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt => opt.DisplayRequestDuration());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
