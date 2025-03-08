using FlashcardXpApi.Common;
using FlashcardXpApi.Data;
using FlashcardXpApi.Extensions;

using FlashcardXpApi.Shared.Mapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var services = WebApplication.CreateBuilder(args);
{

    var config = services.Configuration;
        
    services.Services.AddControllers();
    services.Services.AddEndpointsApiExplorer();
    services.Services.AddSwaggerGenWithAuth();
         
    services.Services.AddExceptionHandling();

    // database
    services.Services.AddDbContext<DataContext>(options =>
    {
        const string FLASHCARDXP_CONTEXT_CONNSTRING = "DefaultConnection";
        options.UseSqlServer(config.GetConnectionString(FLASHCARDXP_CONTEXT_CONNSTRING));
    });
    
    // routing
    services.Services.AddRouting(options => options.LowercaseUrls = true);

    // identity
    services.Services.AddIdentityExtensions();
    
    // features
    services.Services.AddAuthenticationExtensions(config);
    services.Services.AddStudySetExtensions();
    services.Services.AddFlashcardExtensions();

    // mapping and validation
    services.Services.AddAutoMapper(typeof(MappingProfile));
    services.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

    services.Services.AddCors(options =>
    {

        options.AddPolicy("ApiCorsPolicy", policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod();
        });
    });

}

var app = services.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("ApiCorsPolicy");

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseExceptionHandler();


    app.Run();

}



