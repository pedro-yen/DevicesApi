using DevicesApi.BusinessManager.Contracts.Validators.Device;
using DevicesApi.BusinessManager.Services.Devices;
using DevicesApi.Common.Exceptions;
using DevicesApi.Data;
using DevicesApi.Data.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Devices API",
        Version = "v1",
        Description = "REST API for managing devices",
        Contact = new OpenApiContact
        {
            Name = "Pedro Yen",
            Email = "pedro.yen@outlook.com",
            Url = new Uri("https://github.com/pedro-yen")
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

#region DB setup
// Configure SQLite database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString,
        x => x.MigrationsAssembly("DevicesApi.Data")));

#endregion


#region FluentValidation
// Register FluentValidation validators
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<DeviceCreateDtoValidator>();
//builder.Services.AddScoped<IValidator<EmployeeRequestDto>, EmployeeRequestValidator>();
//builder.Services.AddScoped<IValidator<VacationRequestDto>, VacationRequestDtoValidator>();


builder.Services.AddFluentValidationAutoValidation();
#endregion

#region Dependency Injection
// Dependency Injection for Repositories and Managers
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceBusinessManager, DeviceBusinessManager>();

#endregion

#region Logging
builder.Logging.AddConsole();

#endregion
builder.WebHost.UseUrls("http://*:80");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is NotFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(exception.Message);
        }

    });
});

app.Run();