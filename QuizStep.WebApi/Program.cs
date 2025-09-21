
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using QuizStep.Application.Handlers.Test;
using QuizStep.Application.Interfaces;



using Microsoft.IdentityModel.Tokens;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.Profiles;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Config;

using QuizStep.Infrastructure.Data;
using QuizStep.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthorizationHandler, IsTestOwnerHandler>();
builder.Services.AddScoped<ITest, TestRepository>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IEmailSender, FakeEmailService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginUserCommand).Assembly));
builder.Services.AddAutoMapper(cfg => { }, typeof(UserProfile).Assembly);

builder.Services.AddHttpContextAccessor();

// Map Email class with configurations from appsettings.json
var service = builder.Configuration.GetSection("EmailConfig").Get<EmailConfig>();
builder.Services.AddSingleton(service);

builder.Services.AddAuthentication(opts =>
    {
        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opts =>
    {
        opts.SaveToken = true;
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JwtConfig.ISSUER,
            ValidateAudience = true,
            ValidAudience = JwtConfig.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = JwtConfig.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
        
        opts.Events = new JwtBearerEvents
        {
            
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token valid for: " + context.Principal.Identity?.Name);
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();


builder.Services.AddDbContext<ApplicationContext>(opts =>
opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationContext>());

var app = builder.Build();
app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers["Authorization"].ToString();
    Console.WriteLine($"Authorization header received: {authHeader}");
    await next();
});
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();