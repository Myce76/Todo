using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Text;
using System.Xml.Linq;

using Todo.Core.Extensions;

namespace Todo.API.IntegrationTests;

public class CosmoDbRepositoryTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CosmoDbRepositoryTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAll_EndpointsReturnSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/Todos");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
    }

    [Fact]
    public async Task Create_EndpointsReturnSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = new StringContent("\"Teszt description\"", Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("api/Todos", content);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
    }
}
