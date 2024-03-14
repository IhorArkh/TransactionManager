using TransactionManager.Application.Interfaces;
using TransactionManager.Application.Services;
using TransactionManager.Application.Services.CsvHelperService;
using TransactionManager.Application.TransactionRecord.Commands.AddTransactionRecord;
using TransactionManager.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new ApplicationException("Connection string is null.");
builder.Services.AddDataServices(connectionString);
builder.Services.AddMediatR(x =>
    x.RegisterServicesFromAssembly(typeof(AddTransactionRecordCommand).Assembly));
builder.Services.AddScoped<ICsvHelperService, CsvHelperService>();
builder.Services.AddScoped<ITransactionRecordsService, TransactionRecordsService>();
builder.Services.AddScoped<ITimeZoneService, TimeZoneService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();