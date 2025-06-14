using BugTracker.Application.Common.Behaviors;
using BugTracker.Application.Interfaces;
using BugTracker.Application.Projects.Commands.CreateProject;
using BugTracker.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();