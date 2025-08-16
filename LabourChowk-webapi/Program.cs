using LabourChowk_webapi.Data;
using Microsoft.EntityFrameworkCore;
using LabourChowk_webapi.Repositories;
using LabourChowk_webapi.Services;
using AutoMapper; // Add this for AutoMapper

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // ✅ enable controllers

// Register repositories
builder.Services.AddScoped(typeof(GenericRepository<>));

// Register services
builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<WorkerService>();
builder.Services.AddScoped<WorkPosterService>();
//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program)); // ✅ register AutoMapper profiles

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ✅ Add DbContext with SQLite
builder.Services.AddDbContext<LabourChowkContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)); // 👈 Log SQL to console;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); // ✅ maps your controller routes

app.Run();
