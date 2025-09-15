using Application.Mappers;
using Application.Services;
using AutoMapper;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Presentation.IdentitySeed;
using FluentValidation;
using Domain.DTOs;
using Application.Validators;
using Application.Validators.BusinessValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "TooliRent API",
        Version = "v1",
        Description = "An API for managing tool borrowing service"
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put ** _ONLY_** your JWT Bearer token on textbox below!",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {  jwtSecurityScheme, Array.Empty<string>() } 
    });


});


// DBContext

builder.Services.AddDbContext<ToolContext>(options => 
                                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS
var CorsPolicy = "CorsPolicy";
builder.Services.AddCors(opt => 
{
    opt.AddPolicy(name: CorsPolicy, policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:5174") 
              .AllowCredentials();
    });
});

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
    {
        opt.Password.RequireDigit = true;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = true;
        opt.Password.RequireLowercase = false;

        opt.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ToolContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


// Authentication & Authorization

var jwt = builder.Configuration.GetSection("Jwt");
var keyString = jwt["Key"];
Console.WriteLine(keyString);
if (string.IsNullOrWhiteSpace(keyString))
    throw new Exception("JWT key is missing or empty!");
Console.WriteLine($"JWT Key Length: {keyString.Length}");
var key = Encoding.UTF8.GetBytes(jwt["Key"]); 

if(key == null)
{
    throw new Exception("key null?");
}

builder.Services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt["Issuer"],
                        ValidAudience = jwt["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

builder.Services.AddAuthorization(opt =>
                {
                    opt.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                    opt.AddPolicy("UserPolicy", policy => policy.RequireRole("User", "Admin"));
                });


// Data Validation 

builder.Services.AddValidatorsFromAssemblyContaining<BookingCreateDTO_Validator>();

// Business Validation

builder.Services.AddScoped<IBorrower_Validation, Borrower_Validation>();
builder.Services.AddScoped<IBooking_Validation, Booking_Validation>();
builder.Services.AddScoped<ITool_Validation, Tool_Validation>();

// Automapper

builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<ToolTypeMapConfig>();
    cfg.AddProfile<ToolMapConfig>();
    cfg.AddProfile<BookingMapConfig>();
    cfg.AddProfile<BorrowerMapConfig>();
});

builder.Services.AddScoped<IMapper, Mapper>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Services

builder.Services.AddScoped<IToolTypeService, ToolTypeService>();
builder.Services.AddScoped<IToolService, ToolService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBorrowerService, BorrowerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsPolicy);
// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();

IdentityDataSeeder.SeedAsync(app.Services).Wait();

app.Run();
