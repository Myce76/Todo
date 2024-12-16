using Todo.Application.DTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem> Add(TodoItem entity);
        Task<TodoItem> Update(TodoItem entity);
        Task Delete(Guid entityId);
        Task<IEnumerable<TodoItem>> GetAll(string? description, ItemStatus? status, int? pageNumber);
        Task<TodoItem?> GetById(Guid entityId);
        Task<bool> IsExists(Guid entityId);
    }
}
