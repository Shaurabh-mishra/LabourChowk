using LabourChowk_webapi.Data;
using Microsoft.EntityFrameworkCore;
using LabourChowk_webapi.Repositories;
using LabourChowk_webapi.Services;
using AutoMapper;
using LabourChowk_webapi.Services.Interfaces;
using LabourChowk_webapi.Reporsitories.Interfaces;
using Microsoft.AspNetCore.Identity;
using LabourChowk_webapi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using LabourChowk_webapi.Middleware;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register services
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IWorkPosterService, WorkPosterService>();
builder.Services.AddScoped<AuthService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// DbContext with SQLite
builder.Services.AddDbContext<LabourChowkContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));

// Identity-style password hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// JWT Authentication configuration
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSection["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1),
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Role-based Authorization
builder.Services.AddAuthorization();

// Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LabourChowk API", Version = "v1" });

    // JWT Bearer Authorization
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token.\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

// Configure Serilog
// Log.Logger = new LoggerConfiguration()
// //.MinimumLevel.Error()
//     .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day,
//                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
//     .CreateLogger();

// builder.Host.UseSerilog();
// Serilog configuration
// try
// {
//     builder.Host.UseSerilog((context, services, configuration) =>
//  {
//      configuration
//          .MinimumLevel.Debug()
//          .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//          .Enrich.FromLogContext()
//          .WriteTo.Console() // Console logging
//          .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day,
//              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"); // File logging
//  });
// }
// catch (Exception ex)
// {
//     Console.WriteLine("Error configuring Serilog: " + ex.Message);
//     throw;
// }


var app = builder.Build();

// Ensure DB exists and seed admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LabourChowkContext>();
    context.Database.Migrate();

    var hasher = services.GetRequiredService<IPasswordHasher<User>>();

    if (!context.Users.Any(u => u.Username == "admin"))
    {
        var admin = new User
        {
            Username = "admin",
            Role = "Admin",
            CreatedAt = DateTime.UtcNow,
            PasswordHash = hasher.HashPassword(null, "Admin@123")
        };
        context.Users.Add(admin);
        context.SaveChanges();
    }
}

// Middleware pipeline
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LabourChowk API v1");
});
//}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalException>();

app.UseAuthentication(); // Must come before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
