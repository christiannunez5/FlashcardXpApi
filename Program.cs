using FlashcardXpApi.Auth;
using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Common;
using FlashcardXpApi.Data;
using FlashcardXpApi.Exceptions;
using FlashcardXpApi.FlashcardSets;
using FlashcardXpApi.Mapper;
using FlashcardXpApi.Users;
using FlashcardXpApi.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    builder.Services.AddDbContext<DataContext>(options =>
    {
        const string FLASHCARDXP_CONTEXT_CONNSTRING = "DefaultConnection";
        options.UseSqlServer(builder.Configuration.GetConnectionString(FLASHCARDXP_CONTEXT_CONNSTRING));
    });

    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<AuthService>();
    builder.Services.AddScoped<IStudySetRepository, StudySetRepository>();
    builder.Services.AddScoped<StudySetService>();

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
    app.UseAuthorization();
    app.MapControllers();
    app.UseExceptionHandler();
    app.Run();

}



