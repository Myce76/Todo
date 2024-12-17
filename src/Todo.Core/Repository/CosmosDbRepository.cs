using Microsoft.Azure.Cosmos;

using Todo.Application.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Core.Repository;

public class CosmosDbRepository : ITodoRepository
{
    private readonly Container _container;

    public CosmosDbRepository(CosmosClient cosmosDbClient, string databaseName, string containerName)
    {
        _container = cosmosDbClient.GetContainer(databaseName, containerName);
    }

    public async Task<TodoItem> AddAsync(TodoItem entity)
    {
        var item = await _container.CreateItemAsync(entity, new PartitionKey(entity.Id));
        return item.Resource;
    }

    public async Task DeleteAsync(string id)
    {
        await _container.DeleteItemAsync<TodoItem>(id, new PartitionKey(id));
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync(string? queryString)
    {
        var query = _container.GetItemQueryIterator<TodoItem>(new QueryDefinition(queryString));
        var results = new List<TodoItem>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;

    }

    public async Task<TodoItem?> GetByIdAsync(string entityId)
    {
        try
        {
            var response = await _container.ReadItemAsync<TodoItem>(entityId, new PartitionKey(entityId));
            return response.Resource;
        }
        catch (CosmosException) //For handling item not found and other exceptions
        {
            return null;
        }
    }

    public async Task<TodoItem?> UpdateAsync(string entityId, TodoItem entity)
    {
        var item = await _container.UpsertItemAsync(entity, new PartitionKey(entityId));
        if (item != null)
        {
            return item.Resource;
        }
        return null;
    }
 }