using Dapper;

namespace ArcadeOS.Data.Context;

public class DatabaseInitializer
{
    private readonly DbConnectionFactory _factory;
    public DatabaseInitializer(DbConnectionFactory factory) => _factory = factory;

    public async Task InitializeAsync()
    {
        await using var conn = await _factory.CreateOpenConnectionAsync();
        await conn.ExecuteAsync("PRAGMA journal_mode = WAL;");

        var current = await conn.ExecuteScalarAsync<int>("PRAGMA user_version;");
        var assembly = typeof(DatabaseInitializer).Assembly;

        var scripts = assembly.GetManifestResourceNames()
            .Where(n => n.Contains(".Migrations.") && n.EndsWith(".sql"))
            .Select(n => new { Name = n, Version = ParseVersion(n) })
            .Where(s => s.Version > current)
            .OrderBy(s => s.Version);

        foreach (var script in scripts)
        {
            using var stream = assembly.GetManifestResourceStream(script.Name)!;
            using var reader = new StreamReader(stream);
            var sql = await reader.ReadToEndAsync();

            await using var tx = await conn.BeginTransactionAsync();
            await conn.ExecuteAsync(sql, transaction: tx);
            await conn.ExecuteAsync($"PRAGMA user_version = {script.Version};", transaction: tx);
            await tx.CommitAsync();
        }
    }

 
    private static int ParseVersion(string resourceName)
    {
        var file = resourceName.Split('.')[^2];          
        return int.Parse(file[1..file.IndexOf("__")]);
    }
}