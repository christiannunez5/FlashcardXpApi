using FlashcardXpApi.Auth;
using FlashcardXpApi.Data;
using FlashcardXpApi.Exceptions;
using FlashcardXpApi.Flashcards;
using FlashcardXpApi.FlashcardSets;
using FlashcardXpApi.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FlashcardXpApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddAuthenticationExtensions(this IServiceCollection services, 
            IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        ctx.Request.Cookies.TryGetValue("accessToken", out var accessToken);
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            ctx.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },

                };
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<AuthService>();
            services.AddSingleton<JwtHandler>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddHttpContextAccessor();

            return services;
        }

        public static IServiceCollection AddStudySetExtensions(this IServiceCollection services)
        {
            services.AddScoped<IStudySetRepository, StudySetRepository>();
            services.AddScoped<StudySetService>();

            return services;
        }


        public static IServiceCollection AddFlashcardExtensions(this IServiceCollection services)
        {
            services.AddScoped<IFlashcardRepository, FlashcardRepository>();
            services.AddScoped<FlashcardService>();
            return services;
        }
        
        public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }

        public static IServiceCollection AddIdentityExtensions(this IServiceCollection services)
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
                .AddErrorDescriber<AppErrorDescriber>()
                .AddEntityFrameworkStores<DataContext>();

            return services;
        }
    }
}
