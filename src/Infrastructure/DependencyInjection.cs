using System.Text;
using Application.Common.Abstraction;
using Domain.Entities.Users;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthenticationInternal(config);
        services.AddIdentityExtensions();
        services
            .AddTransient<AiAuthHandler>()
            .AddScoped<ICookieService, CookieService>()
            .AddScoped<IEventService, EventService>()
            .AddScoped<IFileToTextService, FileToTextService>()
            .AddScoped<IAiService, AiService>()
            .AddScoped<IDateTimeProvider, DateTimeProvider>();
        
        services
            .AddHttpClient<IAiService, AiService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
            })
            .AddHttpMessageHandler<AiAuthHandler>();
        
        services.AddDatabase(config);
        services
            .AddSignalR();
        
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            const string FLASHCARDXP_CONTEXT_CONNSTRING = "DefaultConnection";
            options.UseSqlServer(config.GetConnectionString(FLASHCARDXP_CONTEXT_CONNSTRING));
        });
        
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        return services;
    }
    private static IServiceCollection AddAuthenticationInternal(this IServiceCollection services,IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["JwtSettings:Issuer"]!,
                ValidAudience = config["JwtSettings:Audience"]!,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Request.Cookies.TryGetValue("accessToken", out var accessToken);

                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;

                },

            };
        });
        
        services.AddCors(options =>
        {
            options.AddPolicy("ApiCorsPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod();
            });
        });
  
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ITokenProvider, TokenProvider>();

        return services;
    }
    
    private static IServiceCollection AddIdentityExtensions(this IServiceCollection services)
    {
        services.AddIdentityApiEndpoints<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }
    
}