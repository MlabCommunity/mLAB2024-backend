using QuizBackend.Application.Extensions;
using QuizBackend.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddInfrastracture(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();