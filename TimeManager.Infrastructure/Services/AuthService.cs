using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TimeManager.Application.Interfaces; // A sua interface
using TimeManager.Domain.Entities;        // O seu User do domínio
using TimeManager.Infrastructure.Identity;

namespace TimeManager.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task RegisterAsync(User domainUser, string password)
    {
        // O PULO DO GATO: Vinculamos as duas tabelas usando o MESMO Guid gerado pelo Domínio.
        var appUser = new ApplicationUser
        {
            Id = domainUser.Id, 
            UserName = domainUser.Email,
            Email = domainUser.Email
        };

        var result = await _userManager.CreateAsync(appUser, password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Falha ao criar credenciais: {errors}");
        }
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        // 1. Busca o usuário pelo e-mail
        var user = await _userManager.FindByEmailAsync(email);
        
        // 2. Valida a senha matematicamente contra o Hash do banco
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
        }

        // 3. Se passou, gera o crachá de acesso (JWT)
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        // A chave secreta que assinará o token (vamos configurar no appsettings.json a seguir)
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                // ClaimTypes.NameIdentifier é o padrão oficial do .NET para guardar o ID
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!)
            }),
            Expires = DateTime.UtcNow.AddHours(8), // Duração do Token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}