using Microsoft.EntityFrameworkCore;
using Persistence;
using API;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
   builder.Services.AddApplicationservices(builder.Configuration);


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("CorsPolicy");

    app.UseAuthorization();

    app.MapControllers();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedData(context);
    }
    catch (System.Exception ex)
    {
        
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex,"An Error Occurred during migration.");
        
    }

    app.Run();
