using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalistTasks.Domain.Dto;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Services;

namespace MinimalistTasks.Controller;

[Route("[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ILogger<ITodo> _logger;
    private readonly ITodoService _service;

    public TodoController(ILogger<ITodo> logger, ITodoService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<TodoDto>> GetAll()
    {
        return await _service.GetAllAsync();
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<TodoDto> GetTodo(int id)
    {
        var obj =  await _service.GetTodoAsync(id);
        return obj;
    }

    [HttpPost]
    [Authorize]
    public async Task<TodoDto> Insert([FromBody] TodoDto todoDto)
    {
        var todo = await _service.InsertAsync(todoDto);
        return todo;
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<TodoDto> Update(int id, [FromBody] NewTodoDto newTodoDto)
    {
        var obj = await _service.UpdateAsync(id, newTodoDto);
        return obj;
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}