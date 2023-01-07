using Dapper;

namespace API.Database;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        switch (_connectionFactory)
        {
            case NpgsqlConnectionFactory:
                await connection.ExecuteAsync(@"
                        CREATE TABLE IF NOT EXISTS Customers (
                        Id UUID PRIMARY KEY, 
                        GitHubUsername TEXT NOT NULL,
                        FullName TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        DateOfBirth TEXT NOT NULL)");
                break;

            case SqlServerConnectionFactory:
                await connection.ExecuteAsync(@"
                        CREATE TABLE  Customers (
                        Id UNIQUEIDENTIFIER PRIMARY KEY, 
                        GitHubUsername NVARCHAR(MAX) NOT NULL,
                        FullName NVARCHAR(MAX) NOT NULL,
                        Email NVARCHAR(MAX) NOT NULL,
                        DateOfBirth NVARCHAR(MAX) NOT NULL)     "
                );
                break;
        }
    }
}
