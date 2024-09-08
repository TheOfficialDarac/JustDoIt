using JustDoIt.DAL;
using JustDoIt.Model;

using JustDoIt.Repository;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Abstractions.Common;
using JustDoIt.Service.Definitions;
using JustDoIt.Service.Definitions.Common;
using JustDoIt.Service.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddCors();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DevelopmentConnection");
    options.UseSqlServer(connection);
    options.EnableSensitiveDataLogging();
});
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    // options.SignIn.RequireConfirmedEmail = true;
}
    ).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();


// builder.Services.AddAuthentication()
//   .AddBearerToken(IdentityConstants.BearerScheme);

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
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
