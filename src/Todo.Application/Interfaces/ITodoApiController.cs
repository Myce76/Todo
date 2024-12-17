using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Todo.Application.DTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Interfaces;
public interface ITodoApiController
{
    /// <summary>
    /// Get all entities with filtering
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAll(string? description, ItemStatus? status, uint? pageNumber);

    /// <summary>
    /// Get one item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<ActionResult<TodoItemDTO>> Get(Guid id);

    /// <summary>
    /// Create an item
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<ActionResult<TodoItemDTO>> Create(string description);

    /// <summary>
    /// Modify an existing item
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<ActionResult<TodoItemDTO>> Update(Guid id, TodoItemDTO dto);

    /// <summary>
    /// Delete an item
    /// </summary>
    /// <param name="id"></param>
    public Task<ActionResult> Delete(Guid id);
}
