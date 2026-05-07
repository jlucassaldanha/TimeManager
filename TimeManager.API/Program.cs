using Microsoft.EntityFrameworkCore;
using TimeManager.Application.UseCases;
using TimeManager.Domain.Interfaces;
using TimeManager.Domain.Services;
using TimeManager.Infrastructure.Data;
using TimeManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITimeRecordRepository, TimeRecordRepository>();
builder.Services.AddScoped<ITimeAllowanceRepository, TimeAllowanceRepository>();
builder.Services.AddScoped<IWorkJourneyRuleRepository, WorkJourneyRuleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<DailyHoursCalculator>();

builder.Services.AddScoped<GetDailySummaryUseCase>();
builder.Services.AddScoped<RegisterRealTimePunchUseCase>();
builder.Services.AddScoped<CreateUserUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();