using Newtonsoft.Json;

namespace Todo.Domain.Entities;

public class TodoItem
{
    [JsonConstructor]
    public TodoItem(string description)
    {
        Id = Guid.NewGuid().ToString();
        Description = description;
        Status = ItemStatus.NotReady;
        CreatedDate = ModifiedDate = DateTime.Now.ToShortDateString();
    }

    // Mapping
    public TodoItem(string id, string description, ItemStatus status)
    {
        Id = id;
        Description = description;
        Status = status;
    }

    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "createddate")]
    public string CreatedDate { get; set; } = DateTime.Now.ToShortDateString();

    [JsonProperty(PropertyName = "modifieddate")]
    public string ModifiedDate { get; set; } = DateTime.Now.ToShortDateString();

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; } = default!;

    [JsonProperty(PropertyName = "status")]
    public ItemStatus Status { get; set; } = ItemStatus.NotReady;
}
