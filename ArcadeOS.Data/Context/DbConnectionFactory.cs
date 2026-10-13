using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ArcadeOS.Data.Context
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string dbPath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
            _connectionString = new SqliteConnectionStringBuilder { DataSource = dbPath }.ToString();
        }

        public async Task<SqliteConnection> CreateOpenConnectionAsync()
        {
            var conn = new SqliteConnection(_connectionString);
            await conn.OpenAsync();
            await conn.ExecuteAsync("PRAGMA foreign_keys = ON; PRAGMA busy_timeout = 5000;");
            return conn;
        }
    }
}
