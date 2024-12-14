using Todo.Application.DTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Interfaces;
public interface IApiController<T> where T : BaseEntity
{
    /// <summary>
    /// Get all entities with filtering
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<IEnumerable<T>>? GetAll(GetRequest<T>? request);

    /// <summary>
    /// Get one item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<T>? Get(string id);

    /// <summary>
    /// Create an item
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<T> Post(T entity);

    /// <summary>
    /// Modify an existing item
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<T>? Put(T entity);

    /// <summary>
    /// Delete an item
    /// </summary>
    /// <param name="id"></param>
    public void Delete(string id);
}
