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
        await connection.ExecuteAsync(@"
        CREATE TABLE  Customers (
        Id UNIQUEIDENTIFIER PRIMARY KEY, 
        GitHubUsername NVARCHAR(MAX) NOT NULL,
        FullName NVARCHAR(MAX) NOT NULL,
        Email NVARCHAR(MAX) NOT NULL,
        DateOfBirth NVARCHAR(MAX) NOT NULL)     ");
    }
}
