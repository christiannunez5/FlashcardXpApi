

using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    var config = builder.Configuration;
    
    var services = builder.Services;
    
    services.AddRouting(options => options.LowercaseUrls = true);
    services.AddSwaggerGen();
    
    services.AddHttpContextAccessor();
    
    services.AddEndpointsApiExplorer();
    
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
    }

    app.UseHttpsRedirection();
    
    app.UseCors("ApiCorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();   
    app.UseExceptionHandler(o => { });

    app.Run();

}



