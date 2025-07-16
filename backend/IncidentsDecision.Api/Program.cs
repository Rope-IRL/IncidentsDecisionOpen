using System.Text;
using IncidentsDecision.Application.Interfaces;
using IncidentsDecision.Application.Services;
using IncidentsDecision.Core.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var corsName = "AllowAny";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsName,
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:8080")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

//builder.Services.AddDbContext<IncidentDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));

// builder.Services.AddDbContext<IncidentDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddDbContext<IncidentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IEmployeeLoginRepository, EmployeeLoginRepository>();
builder.Services.AddScoped<IEmployeeLoginService, EmployeeLoginService>();

builder.Services.AddScoped<IEmployeeHealthGroupRepository, EmployeeHealthGroupRepository>();
builder.Services.AddScoped<IEmployeeHealthGroupService, EmployeeHealthGroupService>();

builder.Services.AddScoped<IEmployeePositionRepository, EmployeePositionRepository>();
builder.Services.AddScoped<IEmployeePositionService, EmployeePositionService>();

builder.Services.AddScoped<IHealthGroupRepository, HealthGroupRepository>();
builder.Services.AddScoped<IHealthGroupService, HealthGroupService>();

builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IPositionService, PositionService>();

builder.Services.AddScoped<ITechSupportRepository, TechSupportRepository>();
builder.Services.AddScoped<ITechSupportService, TechSupportService>();

builder.Services.AddScoped<ITechSupportLoginRepository, TechSupportLoginRepository>();
builder.Services.AddScoped<ITechSupportLoginService, TechSupportLoginService>();

builder.Services.AddScoped<INotResolvedIncidentRepository, NotResolvedIncidentRepository>();
builder.Services.AddScoped<INotResolvedIncidentService, NotResolvedIncidentService>();

builder.Services.AddScoped<IResolvedIncidentRepository, ResolvedIncidentRepository>();
builder.Services.AddScoped<IResolvedIncidentService, ResolvedIncidentService>();

builder.Services.AddSingleton<ITokenProvider,TokenProvider>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            ClockSkew = TimeSpan.Zero
        };

        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies[builder.Configuration["JWT:Name"]];

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();
app.UseCors(corsName);

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

