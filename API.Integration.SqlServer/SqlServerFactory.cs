using API.Database;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API.Integration.SqlServer;

public class SqlServerFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{

    private readonly MsSqlTestcontainer _dbContainer;
    public SqlServerFactory()
    {
        var mssqlConfiguration = new DatabaseContainerConfiguration();
        _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(mssqlConfiguration)
            .Build();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var cnn = _dbContainer.ConnectionString+ "Encrypt=false";//Considering new .net 7.0 rules about security in sqlServer

            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ => new SqlServerConnectionFactory(cnn));
        });
    }
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}