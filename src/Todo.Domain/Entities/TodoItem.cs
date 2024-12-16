namespace Todo.Domain.Entities;

public class TodoItem : BaseEntity
{
    public TodoItem(string description)
    {
        Id = Guid.NewGuid();
        Description = description;
        Status = ItemStatus.NotReady;
    }

    // Mapping
    public TodoItem(Guid id, string description, ItemStatus status)
    {
        Id = id;
        Description = description;
        Status = status;
    }

    public string Description { get; set; } = default!;
    public ItemStatus Status { get; set; } = ItemStatus.NotReady;
}
