using QuizBackend.Api.Extensions;
using QuizBackend.Application.Extensions;
using QuizBackend.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true; 
});

builder.Services.AddInfrastracture(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddExceptionHandlers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();