
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public TodosController(ITodoRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// GET: api/Todos
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAll(FilterDTO request)
    {
        var results = await _repository.GetAll(request.Description, request.Status, request.PageNumber);
        if (results.Any())
        {
            return Ok(results.Select(x => x.ToDTO()));
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
        var todoItem = await _repository.GetById(id);
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
    public async Task<ActionResult<TodoItemDTO>> Create(string description)
    {
        var entity = await _repository.Add(new TodoItem(description));
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
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var itemToUpdate = _repository.GetById(id).Result;
        if (itemToUpdate == null)
        {
            return NotFound();
        }

        itemToUpdate.Description = dto.Description;
        itemToUpdate.Status = dto.Status;

        try
        {
            await _repository.Update(itemToUpdate);
        }
        catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        {
            return NotFound();
        }

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
        var itemToDelete = _repository.GetById(id).Result;
        if (itemToDelete == null)
        {
            return NotFound();
        }

        await _repository.Delete(id);

        return NoContent();
        
    }

    private bool TodoItemExists(Guid id)
    {
        var result = Task.Run(async () => await _repository.IsExists(id));
        return result.Result;
        
    }
}
