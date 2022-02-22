using Microsoft.AspNetCore.Mvc;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Domain.Model;

namespace MinimalistTasks.Controller;

[Route("[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ILogger<ITodo> _logger;

    public TodoController(ILogger<ITodo> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<ITodo>> GetTodos()
    {
        IEnumerable<ITodo> todos = new List<ITodo>();
        
        ITodo todoOne = new Todo
        {
            TodoId = 1,
            Text = "Task one",
            CreationDate = DateTime.Now,
            IsCompleted = false
        };
        
        ITodo todoTwo = new Todo
        {
            TodoId = 2,
            Text = "Task two",
            CreationDate = DateTime.Now,
            IsCompleted = true
        };

        todos = todos.Append(todoOne);
        todos = todos.Append(todoTwo);
        
        _logger.LogInformation("Task: {text}", todoOne.Text);

        return todos;
    }
}