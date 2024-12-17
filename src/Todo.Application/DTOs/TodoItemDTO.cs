using Todo.Domain.Entities;

namespace Todo.Application.DTOs;
public class TodoItemDTO
{
    public required string Id { get; set; }
    public required string Description { get; set; }
    public ItemStatus Status { get; set; }
}
