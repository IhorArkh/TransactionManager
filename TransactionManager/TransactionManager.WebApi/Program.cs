using System.Reflection;
using Microsoft.OpenApi.Models;
using TransactionManager.Application.Features.TransactionRecord.Commands.AddTransactionRecord;
using TransactionManager.Application.Interfaces;
using TransactionManager.Application.Services;
using TransactionManager.Application.Services.CsvHelperService;
using TransactionManager.Application.Services.LocationService;
using TransactionManager.Persistence;
using TransactionManager.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TransactionManager API",
        Description = "An ASP.NET Core Web API for managing transactions"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string not found.");
builder.Services.AddDataServices(connectionString);

builder.Services.AddMediatR(x =>
    x.RegisterServicesFromAssembly(typeof(AddTransactionRecordCommand).Assembly));

builder.Services.AddScoped<ICsvHelperService, CsvHelperService>();
builder.Services.AddScoped<ITransactionRecordsService, TransactionRecordsService>();
builder.Services.AddScoped<ITimeZoneService, TimeZoneService>();

string ipInfoToken = builder.Configuration["IpInfoToken"] ??
                     throw new InvalidOperationException("IpInfoToken not found.");
builder.Services.AddScoped<ILocationService>(provider => new LocationService(ipInfoToken));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();