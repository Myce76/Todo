using System.Linq.Expressions;
using Todo.Domain.Entities;

namespace Todo.Application.DTOs;
public class FilterDTO
{
    public string? Description { get; set; }
    public ItemStatus? Status { get; set; }
    public int? PageNumber { get; set; }
}
