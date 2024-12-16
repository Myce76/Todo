using Microsoft.EntityFrameworkCore;

using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Infrastructure.Persistence;

namespace Todo.Core.Repository;

public class CosmosDbRepository : ITodoRepository
{
    //The context is added in Step 5.1
    private readonly TodoContext _context;
    private const int PageSize = 25;

    public CosmosDbRepository(TodoContext context)
    {
        _context = context;
    }

    public async Task<TodoItem> Add(TodoItem entity)
    {
        var addedEntity = (await _context.Todos.AddAsync(entity)).Entity;
        await _context.SaveChangesAsync();
        return addedEntity;
    }

    public async Task Delete(Guid entityId)
    {
        var entity = await _context.Todos.FindAsync(entityId);
        if (entity != null)
            _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TodoItem>> GetAll(string? description, ItemStatus? status, int? pageNumber)
    {
        IQueryable<TodoItem> query = _context.Todos.OrderBy(x => x.Id);

        if (string.IsNullOrEmpty(description) != true)
        {
            query = query.Where(x => x.Description.Contains(description));
        }

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        if (pageNumber.HasValue)
        {
            query = query.Skip((pageNumber.Value-1)*PageSize);
        }

        query = query.Take(PageSize);
        
        return await query.ToListAsync();
    }

    public async Task<TodoItem?> GetById(Guid entityId)
    {
        return await _context.Todos.FindAsync(entityId);
    }

    public async Task<TodoItem> Update(TodoItem entity)
    {
        var updatedEntity = _context.Todos.Update(entity).Entity;
        await _context.SaveChangesAsync();
        return updatedEntity;
    }

    public async Task<bool> IsExists(Guid id)
    {
        return await _context.Todos.AnyAsync(e => e.Id == id);
    }
}