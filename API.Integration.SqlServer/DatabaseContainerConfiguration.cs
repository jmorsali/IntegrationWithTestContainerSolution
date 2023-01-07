﻿using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace API.Integration.SqlServer;

public sealed class DatabaseContainerConfiguration : MsSqlTestcontainerConfiguration
{
    public DatabaseContainerConfiguration() : base("mcr.microsoft.com/mssql/server")
    {
        Password = Guid.NewGuid().ToString("D");
        Database = Guid.NewGuid().ToString("D");
    }

    public override IWaitForContainerOS WaitStrategy { get; }
      = Wait.ForUnixContainer().AddCustomWaitStrategy(new AcceptsClientConnections());

    private sealed class AcceptsClientConnections : IWaitUntil
    {
        public async Task<bool> Until(ITestcontainersContainer testcontainers, ILogger logger)
        {
            var (stdout, _) = await testcontainers.GetLogs()
              .ConfigureAwait(false);
            return stdout.Contains("SQL Server is now ready for client connections.", StringComparison.OrdinalIgnoreCase);
        }
    }
}
