
using FlashcardXpApi.Application;
using FlashcardXpApi.Exceptions;
using FlashcardXpApi.Infrastructure;
using FlashcardXpApi.Infrastructure.Persistence;

var services = WebApplication.CreateBuilder(args);
{
    
    var config = services.Configuration;
        
    services.Services.AddControllers();
    services.Services.AddEndpointsApiExplorer();
         
    services.Services.AddExceptionHandler<GlobalExceptionHandler>();

    services.Services.AddHttpContextAccessor();

    services.Services.AddRouting(options => options.LowercaseUrls = true);

    services.Services.AddInfrastructure(config);

    services.Services.AddApplication();

}

var app = services.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            await Seeder.Initialize(context);
        }

    }

    app.UseHttpsRedirection();

    app.UseCors("ApiCorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();   
    app.UseExceptionHandler(o => { });

    app.Run();

}



