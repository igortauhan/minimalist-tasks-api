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
    private readonly TodoService _service;

    public TodoController(ILogger<ITodo> logger, TodoService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<TodoDto>> GetAll()
    {
        return await _service.GetAllAsync();
    }

    [HttpPost]
    public async Task<TodoDto> Insert([FromBody] TodoDto todoDto)
    {
        var todo = await _service.InsertAsync(todoDto);
        return todo;
    }
}