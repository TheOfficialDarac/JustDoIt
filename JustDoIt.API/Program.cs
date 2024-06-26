using JustDoIt.DAL;
using JustDoIt.Model;

using JustDoIt.Repository;
using JustDoIt.Repository.Common;
using JustDoIt.Service;
using JustDoIt.Service.Common;
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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddCors();
builder.Services.AddDbContext<DataContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DevelopmentConnection");
    options.UseSqlServer(connection);
    options.EnableSensitiveDataLogging();
});
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
}
    )
    .AddEntityFrameworkStores<DataContext>();


// builder.Services.AddAuthentication()
//   .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("api", p =>
  {
      p.RequireAuthenticatedUser();
      p.AddAuthenticationSchemes(IdentityConstants.BearerScheme);
  });

var app = builder.Build();

app.MapGet("/pingauth", (ClaimsPrincipal user) =>
            {
                var email = user.FindFirstValue(ClaimTypes.Email); // get the user's email from the claim
                return Results.Json(new { Email = email }); ; // return the email as a plain text response
            }).RequireAuthorization();


app.MapGroup("api/auth").MapIdentityApi<AppUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
