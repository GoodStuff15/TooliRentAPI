using Application.Mappers;
using Application.Services;
using AutoMapper;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DBContext

builder.Services.AddDbContext<ToolContext>(options => 
                                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Automapper

builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<ToolTypeMapConfig>();
    cfg.AddProfile<ToolMapConfig>();
});

builder.Services.AddScoped<IMapper, Mapper>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Services

builder.Services.AddScoped<IToolTypeService, ToolTypeService>();
builder.Services.AddScoped<IToolService, ToolService>();

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
