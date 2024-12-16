using Todo.Application.DTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem> AddAsync(TodoItem entity);
        Task<TodoItem?> UpdateAsync(string entityId, TodoItem entity);
        Task DeleteAsync(string entityId);
        Task<IEnumerable<TodoItem>> GetAllAsync(string? GetAllAsync);
        Task<TodoItem?> GetByIdAsync(string entityId);
    }
}
