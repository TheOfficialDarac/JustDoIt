//using JustDoIt.API.Services;
using JustDoIt.API.Services;
using JustDoIt.DAL;
using JustDoIt.Model;

using JustDoIt.Repository;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Abstractions.Common;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Definitions;
using JustDoIt.Service.Definitions.Common;
using JustDoIt.Service.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DevelopmentConnection");
    options.UseSqlServer(connection);
    options.EnableSensitiveDataLogging();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
}
);

//builder.Services.AddScoped<IRepository, Repository>();
//builder.Services.AddScoped<IService, Service>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddCors();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    options.Password.RequiredLength = 5;
//    options.User.RequireUniqueEmail = true;
//})
//    .AddEntityFrameworkStores<ApplicationContext>(
//    ).AddDefaultTokenProviders();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
{
    options.Password.RequiredLength = 5;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        RequireExpirationTime = true,
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:key").Value))
    };
});
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("api", p =>
  {
      p.RequireAuthenticatedUser();
      p.AddAuthenticationSchemes(IdentityConstants.BearerScheme);
  });

var app = builder.Build();

app.MapGroup("api/auth").MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
