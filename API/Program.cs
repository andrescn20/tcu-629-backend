using Microsoft.EntityFrameworkCore;
using DataAccess;
using API.Interfaces;
using API.Services;
using DataAccess.Interfaces;
using DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("LocalConnection")
    : builder.Configuration.GetConnectionString("AzureConnection");

builder.Services.AddDbContext<MonitoringDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ICollectedDataService, CollectedDataService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IHardwareService, HardwareService>();
builder.Services.AddScoped<ISensorService, SensorService>();

builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<ICollectedDataRepository, CollectedDataRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IHardwareRepository, HardwareRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy(name: "TCU_Cors",
        policy => {
            policy.WithOrigins("http://localhost:5173", "https://tcu-629-q49rvzp0e-andrescn20s-projects.vercel.app/");
            policy.AllowAnyHeader(); //application/json  application/xml application/text
            policy.AllowAnyMethod(); //GET, POST, PUT, DELETE
        });
});

var app = builder.Build();

// Seed the database if in Development environment
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MonitoringDbContext>();
    DbInitializer.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("TCU_Cors");

app.Run();
