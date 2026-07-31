using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/health-check", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow }));

app.MapControllers();

app.Run();