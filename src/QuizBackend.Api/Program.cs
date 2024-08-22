using QuizBackend.Api.Extensions;
using QuizBackend.Application.Extensions;
using QuizBackend.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddExceptionHandlers();
builder.Services.AddConfigureCors(builder.Configuration);

builder.Services.AddConfigureCors(builder.Configuration);

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigins");

app.MapControllers();

await app.EnsureDatabaseMigratedAsync();

app.Run();