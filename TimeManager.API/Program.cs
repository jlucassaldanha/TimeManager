using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using TimeManager.API.Services;
using TimeManager.Application.Interfaces;
using TimeManager.Application.UseCases;
using TimeManager.Domain.Interfaces;
using TimeManager.Domain.Services;
using TimeManager.Infrastructure.Data;
using TimeManager.Infrastructure.Repositories;
using TimeManager.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

var secretKey = builder.Configuration["Jwt:SecretKey"]
    ?? throw new InvalidOperationException("Jwt:SecretKey não configurado");
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ITimeRecordRepository, TimeRecordRepository>();
builder.Services.AddScoped<ITimeAllowanceRepository, TimeAllowanceRepository>();
builder.Services.AddScoped<IWorkJourneyRuleRepository, WorkJourneyRuleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

builder.Services.AddScoped<DailyHoursCalculator>();
builder.Services.AddScoped<AllowanceService>();

builder.Services.AddScoped<GetDailySummaryUseCase>();
builder.Services.AddScoped<GetPeriodSummaryUseCase>();
builder.Services.AddScoped<RegisterRealTimePunchUseCase>();
builder.Services.AddScoped<RegisterManualPunchUseCase>();
builder.Services.AddScoped<CreateWorkJourneyRuleUseCase>();
builder.Services.AddScoped<UpdatePunchUseCase>();
builder.Services.AddScoped<UpdateWorkJourneyRuleUseCase>();
builder.Services.AddScoped<DeletePunchUseCase>();
builder.Services.AddScoped<GetAllowanceEligibilityUseCase>();
builder.Services.AddScoped<CreateAllowanceUseCase>();
builder.Services.AddScoped<GetWorkJourneyRuleUseCase>();
builder.Services.AddScoped<GetAllowanceUseCase>();
builder.Services.AddScoped<UpdateAllowanceUseCase>();
builder.Services.AddScoped<DeleteAllowanceUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeManager API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT desta forma: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();