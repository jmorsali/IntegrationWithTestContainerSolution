using API.Database;
using API.Repositories;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WeatherForecast;
using Xunit;

namespace API.Tests.Integration;

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
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ => new SqlServerConnectionFactory(_dbContainer.ConnectionString));


            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<SaleDbStore>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<SaleDbStore>(options => options.UseSqlServer(_dbContainer.ConnectionString + "TrustServerCertificate=True;"));
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