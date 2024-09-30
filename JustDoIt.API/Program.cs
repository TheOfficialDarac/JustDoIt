using JustDoIt.DAL;
using JustDoIt.Model.Database;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Implementations;
using JustDoIt.Service;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Abstractions.Common;
using JustDoIt.Service.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DevelopmentConnection");
    options.UseSqlServer(connection);
    options.EnableSensitiveDataLogging();
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "JWT Authentification",
            Description = "Wnter you JWT token in this field",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT"
        };
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
        //options.OperationFilter<SecurityRequirementsOperationFilter>();

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                []
            }
        };

        options.AddSecurityRequirement(securityRequirement);
        //    options.SwaggerDoc("v1", new OpenApiInfo { Title = "JustDoIt API", Version = "v1", Description = "API for the JustDoIt Web App" });
        //    var security = new Dictionary<string, IEnumerable<string>>
        //    {
        //        {"Bearer", new string[0] }
        //    };

        //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{ Description = "JWT Authorization header using the bearer scheme", Name = "Authorization", In = ParameterLocation.Header, Type = SecuritySchemeType.ApiKey });
    }
);

//builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
//{
//    options.User.RequireUniqueEmail = true;
//    // options.SignIn.RequireConfirmedEmail = true;
//}
//    ).AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    //opts.SignIn.RequireConfirmedEmail = true;
    opts.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();

builder.Services.AddScoped<IUtilsRepository, UtilsRepository>();
builder.Services.AddScoped<IUtilsService, UtilsService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<TokenProvider>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = false;
    option.SaveToken = true;
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!)),
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value!,
        ValidAudiences = builder.Configuration.GetSection("Jwt:Audience").Value!.Split(";"),
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

//builder.Services.AddAuthorization( opt => {
//    opt.AddPolicy(IdentityData.AdminUserPolicyName, p => p.RequireClaim(IdentityData.AdminUserClaimName));
//});

builder.Services.AddCors();

var app = builder.Build();

//app.MapGroup("api/auth").MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();