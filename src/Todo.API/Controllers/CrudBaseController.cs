using Microsoft.AspNetCore.Mvc;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;

namespace Todo.API.Controllers;

//public class CrudBaseController<T> : ControllerBase, ITodoApiController<T>
//    where T : BaseEntity
//{
//    private readonly ITodoRepository _repository;

//    public CrudBaseController(ITodoRepository repository)
//    {
//        _repository = repository;
//    }

//    [HttpGet]
//    public async Task<IEnumerable<T>>? GetAll([FromQuery] GetRequest<T>? request = null)
//    {
//        return await _repository.GetAll(request);
//    }

//    [HttpGet("{id}")]
//    public async Task<T>? Get(Guid id)
//    {
//        return await _repository.GetById(id.ToString());
//    }

//    [HttpPost]
//    public async Task<T> Post(T entity)
//    {
//        return await _repository.Add(entity);
//    }

//    [HttpPut]
//    public async Task<T>? Put(T entity)
//    {
//        return await _repository.Update(entity);
//    }

//    [HttpDelete("{id}")]
//    public void Delete(Guid id)
//    {
//        _repository.Delete(id.ToString());
//    }

//}