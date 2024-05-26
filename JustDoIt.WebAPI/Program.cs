using JustDoIt.DAL;
using JustDoIt.Repository;
using JustDoIt.Repository.Common;
using JustDoIt.Service;
using JustDoIt.Service.Common;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddCors();
builder.Services.AddDbContext<DataContext>(options => {
    var connection = builder.Configuration.GetConnectionString("DevelopmentConnection");
    options.UseSqlServer(connection);
    options.EnableSensitiveDataLogging();
});

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

app.Run();
