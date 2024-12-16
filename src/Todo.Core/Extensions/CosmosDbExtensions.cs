using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Todo.Application.Interfaces;
using Todo.Core.Repository;

namespace Todo.Core.Extensions;

public static class CosmosDbExtensions
{
    private static async Task<CosmosDbRepository> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
    {
        var databaseName = configurationSection["DatabaseName"];
        var containerName = configurationSection["ContainerName"];
        var account = configurationSection["Account"];
        var key = configurationSection["Key"];

        var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
        var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

        var cosmosDbService = new CosmosDbRepository(client, databaseName, containerName);
        return cosmosDbService;
    }

    public static void ConfigureCosmosDBRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITodoRepository>(InitializeCosmosClientInstanceAsync(configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
    }
}
