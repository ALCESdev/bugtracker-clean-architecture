using BugTracker.Application.Interfaces;
using BugTracker.Application.Projects.Commands.CreateProject;
using BugTracker.Infrastructure.Persistence;
using BugTracker.WebAPI.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Configurar opciones de columnas evitando duplicadas
    ColumnOptions? columnOptions = new();

    // Logger configuration
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Less verbose logging
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.MSSqlServer(
            connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
            sinkOptions: new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            },
            columnOptions: columnOptions
        )
        .CreateLogger();

    builder.Logging.ClearProviders();
    builder.Host.UseSerilog();

    // DbContext
    builder.Services.AddDbContext<IBugTrackerDbContext, BugTrackerDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // MediatR
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblyContaining<CreateProjectCommand>());

    // FluentValidation
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblyContaining<IApplicationMarker>());

    builder.Services.AddControllers()
        .AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssemblyContaining<IApplicationMarker>();
        });

    // Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "BugTracker API", Version = "v1" });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BugTrackerDbContext>();
        context.Database.Migrate();
        DataSeeder.Seed(context);

        var config = builder.Configuration;
        bool resetDb = config.GetValue<bool>("ResetDbOnStartup");

        if (resetDb)
        {
            await DatabaseInitializer.ResetAsync(app.Services);
        }

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.MapControllers();

    Log.Information("Iniciando BugTracker...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación BugTracker no pudo iniciarse correctamente.");
}
finally
{
    Log.CloseAndFlush();
}