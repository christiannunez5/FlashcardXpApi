using FlashcardXpApi.Auth;
using FlashcardXpApi.Common;
using FlashcardXpApi.Data;
using FlashcardXpApi.Exceptions;
using FlashcardXpApi.Flashcards;
using FlashcardXpApi.FlashcardSets;
using FlashcardXpApi.Mapper;
using FlashcardXpApi.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
{
    var config = builder.Configuration;

    builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        };
        o.Events = new JwtBearerEvents
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
    
    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
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
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    builder.Services.AddDbContext<DataContext>(options =>
    {
        const string FLASHCARDXP_CONTEXT_CONNSTRING = "DefaultConnection";
        options.UseSqlServer(config.GetConnectionString(FLASHCARDXP_CONTEXT_CONNSTRING));
    });

    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddScoped<AuthService>();
    builder.Services.AddSingleton<JwtHandler>();
    builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
    builder.Services.AddIdentityApiEndpoints<User>(options =>
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
    builder.Services.AddSingleton<TokenProvider>();

    builder.Services.AddScoped<IStudySetRepository, StudySetRepository>();
    builder.Services.AddScoped<StudySetService>();
    builder.Services.AddScoped<IFlashcardRepository, FlashcardRepository>();
    builder.Services.AddScoped<FlashcardService>();

    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseExceptionHandler();

    app.Run();

}



