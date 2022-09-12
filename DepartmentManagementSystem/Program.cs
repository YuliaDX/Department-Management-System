using AutoMapper;
using Core.Domain;
using Core.Interfaces;
using DataAccess.Data;
using DataAccess.Repositories;
using DepartmentManagementSystem.AutoMapperProfiles;
using DepartmentManagementSystem.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers();
services.AddScoped(typeof(IRepository<Department>), (x=> new InMemoryRepository<Department>(DataFactory.Departments)));

services.AddAutoMapper(new[] {typeof(DepartmentProfile), typeof(UserProfile) } );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
