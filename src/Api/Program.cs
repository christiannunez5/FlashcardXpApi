using Api;
using Application;
using Application.Common.Abstraction;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
{
    var config = builder.Configuration;
    var services = builder.Services;
    
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddRouting(options => options.LowercaseUrls = true);
    services.AddSwaggerGen();
    services.AddHttpContextAccessor();
    services.AddExceptionHandler<GlobalExceptionHandler>();
    services
        .AddInfrastructure(config)
        .AddApplication();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.ApplyMigrations();
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        await Seeder.Initialize(dbContext, CancellationToken.None);
    }
    
    app.UseHttpsRedirection();
    
    app.UseCors("ApiCorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.MapHub<EventHub>("/hub/events");
    
    app.UseExceptionHandler(o => { });
    
    app.Run();

}



