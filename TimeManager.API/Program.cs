using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeManager.API.Services;
using TimeManager.Application.Interfaces;
using TimeManager.Application.UseCases;
using TimeManager.Domain.Interfaces;
using TimeManager.Domain.Services;
using TimeManager.Infrastructure.Data;
using TimeManager.Infrastructure.Identity;
using TimeManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITimeRecordRepository, TimeRecordRepository>();
builder.Services.AddScoped<ITimeAllowanceRepository, TimeAllowanceRepository>();
builder.Services.AddScoped<IWorkJourneyRuleRepository, WorkJourneyRuleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<DailyHoursCalculator>();
builder.Services.AddScoped<AllowanceService>();

builder.Services.AddScoped<GetDailySummaryUseCase>();
builder.Services.AddScoped<GetPeriodSummaryUseCase>();
builder.Services.AddScoped<RegisterRealTimePunchUseCase>();
builder.Services.AddScoped<RegisterManualPunchUseCase>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<CreateWorkJourneyRuleUseCase>();
builder.Services.AddScoped<UpdatePunchUseCase>();
builder.Services.AddScoped<UpdateWorkJourneyRuleUseCase>();
builder.Services.AddScoped<DeletePunchUseCase>();
builder.Services.AddScoped<GetAllowanceEligibilityUseCase>();
builder.Services.AddScoped<CreateAllowanceUseCase>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

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