using FluentAssertions;

using Todo.Application.DTOs;
using Todo.Application.Extensions;
using Todo.Domain.Entities;

namespace Todo.API.UnitTests;

public class TodoItemMappingExtensionsTests
{
    [Fact]
    public void ToEntity_CallToTodoItemDTO_ShoulGiveBackProperTodoItem()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var dto = new TodoItemDTO { Id = guid.ToString(), Description = "Test", Status = Domain.Entities.ItemStatus.Ready };

        //Act
        var ret = dto.ToEntity();

        //Assert
        ret.Status.Should().Be(ItemStatus.Ready);
        ret.Description.Should().Be("Test");
        ret.Id.Should().Be(guid.ToString());
    }

    [Fact]
    public void ToDTO_CallToTodoItem_ShoulGiveBackProperTodoItemDTO()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var todoItem = new TodoItem( guid.ToString(), "Test", ItemStatus.Ready);

        //Act
        var ret = todoItem.ToDTO();

        //Assert
        ret.Status.Should().Be(ItemStatus.Ready);
        ret.Description.Should().Be("Test");
        ret.Id.Should().Be(guid.ToString());
    }
}