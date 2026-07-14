using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Application.Interfaces.IServices;
using RealEstate.Application.Services.Agents;
using RealEstate.Application.Services.Auth;
using RealEstate.Application.Services.Clients;
using RealEstate.Application.Services.Properties;
using RealEstate.Application.Services.Users;
using RealEstate.Infrastructure.Persistence;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Services.Auth;

namespace RealEstate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IPropertyService, PropertyService>();

        services.AddScoped<IAgentRepository, AgentRepository>();
        services.AddScoped<IAgentService, AgentService>();

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IClientService, ClientService>();
        
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRefreshTokenRepository,
            RefreshTokenRepository>();

        services.AddScoped<IUserService,
            UserService>();

        services.AddScoped<IAuthService,
            AuthService>();

        services.AddScoped<IJwtTokenGenerator,
            JwtTokenGenerator>();

        services.AddScoped<IRefreshTokenGenerator,
            RefreshTokenGenerator>();
        
        return services;
    }
}