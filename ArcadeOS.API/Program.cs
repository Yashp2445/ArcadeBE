using ArcadeOS.Data.Context;

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;  

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var dbPath = builder.Configuration["ARCADEOS_DB_PATH"]
    ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "ArcadeOS", "arcadeos.db");

builder.Services.AddSingleton(new DbConnectionFactory(dbPath));
builder.Services.AddSingleton<DatabaseInitializer>();

var app = builder.Build();

await app.Services.GetRequiredService<DatabaseInitializer>().InitializeAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapControllers();

app.Run();