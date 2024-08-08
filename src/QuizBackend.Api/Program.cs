using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;
using QuizBackend.Infrastructure.Data;
using QuizBackend.Infrastructure.Extensions;
using QuizBackend.Infrastructure;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastracture(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();