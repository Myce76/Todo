
using Microsoft.AspNetCore.Mvc;

using Todo.Application.DTOs;
using Todo.Application.Extensions;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;

namespace Todo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase, ITodoApiController
{
    private readonly ITodoRepository _repository;
    private const uint PageSize = 25;

    public TodosController(ITodoRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// GET: api/Todos
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAll(string? description, ItemStatus? status, uint? pageNumber)
    {
        //var results = await _repository.GetAll(request.Description, request.Status, request.PageNumber);
        var select = string.Empty;
        select = select.GetQueryString(description, status, pageNumber, PageSize);
        var results = await _repository.GetAllAsync(select);
        if (results.Any())
        {
            return Ok(results.Select(x => x.ToDTO()).AsEnumerable());
        }
        return Ok(Enumerable.Empty<TodoItemDTO>()); 
    }

    /// <summary>
    /// GET api/Todos/1
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("({id})")]
    public async Task<ActionResult<TodoItemDTO>> Get([FromRoute] Guid id)
    {
        var todoItem = await _repository.GetByIdAsync(id.ToString());
        if (todoItem == null)
        {
            return NotFound();
        }
        return Ok(todoItem.ToDTO());
    }

    /// <summary>
    /// POST api/Todos
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> Create([FromBody] string description)
    {
        var entity = await _repository.AddAsync(new TodoItem(description));
        var dto = entity.ToDTO();
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    /// <summary>
    /// PUT api/Todos/1
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<TodoItemDTO>> Update([FromRoute] Guid id, TodoItemDTO dto)
    {
        var entId = id.ToString();
        if (entId != dto.Id)
        {
            return BadRequest();
        }

        var itemToUpdate = _repository.GetByIdAsync(entId).Result;
        if (itemToUpdate == null)
        {
            return NotFound();
        }

        itemToUpdate.Description = dto.Description;
        itemToUpdate.Status = dto.Status;

        await _repository.UpdateAsync(entId, itemToUpdate);
        
        return NoContent();
    }

    /// <summary>
    /// DELETE api/todos/1
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        var entId = id.ToString();
        var itemToDelete = _repository.GetByIdAsync(entId).Result;
        if (itemToDelete == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(entId);

        return NoContent();
        
    }
}
