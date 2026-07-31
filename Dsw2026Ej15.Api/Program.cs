using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Dsw2026Ej15DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersistence, PersistenceEf>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<Dsw2026Ej15DbContext>();
        dbContext.Database.EnsureCreated();

        if (!dbContext.Specialities.Any())
        {
            string[] possiblePaths =
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "specialities.json"),
                "specialities.json",
                Path.Combine(Directory.GetCurrentDirectory(), "specialities.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "specialities.json")
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    var list = JsonSerializer.Deserialize<List<Speciality>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (list != null && list.Count > 0)
                    {
                        dbContext.Specialities.AddRange(list);
                        dbContext.SaveChanges();
                        break;
                    }
                }
            }
        }
    }
    catch
    {
        // Fallback for environment without LocalDB
    }
}

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