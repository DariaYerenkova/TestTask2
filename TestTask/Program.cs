using Microsoft.EntityFrameworkCore;
using RestSharp;
using TestTask.Domain;
using TestTask.Domain.Ropositories;
using TestTask.Domain.Ropositories.Interfaces;
using TestTask.Services;
using TestTask.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TimerDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString"), b => b.MigrationsAssembly("TestTask")));

builder.Services.AddScoped<ITimerWebHookRepository, TimerWebHookRepository>();
builder.Services.AddScoped<ITimerService, TimerService>();
//builder.Services.AddHostedService<BackgroundTimerService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
