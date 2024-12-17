using Todo.Application.DTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Extensions;
public static class TodoItemMappingExtensions
{
    public static TodoItem ToEntity(this TodoItemDTO dto)
    {
        return new TodoItem(dto.Id, dto.Description, dto.Status);
    }

    public static TodoItemDTO ToDTO(this TodoItem entity)
    {
        return new TodoItemDTO
        {
            Id = entity.Id,
            Description = entity.Description,
            Status = entity.Status,
        };
    }
}
