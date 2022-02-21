using Microsoft.AspNetCore.Mvc;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Domain.Model;

namespace MinimalistTasks.Controller;

[Route("[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ITodo>> GetTodos()
    {
        IEnumerable<ITodo> todos = new List<ITodo>();
        
        ITodo todoOne = new Todo
        {
            Id = 1,
            Text = "Task one",
            CreationDate = DateTime.Now,
            IsCompleted = false
        };
        
        ITodo todoTwo = new Todo
        {
            Id = 2,
            Text = "Task two",
            CreationDate = DateTime.Now,
            IsCompleted = true
        };

        todos = todos.Append(todoOne);
        todos = todos.Append(todoTwo);

        return todos;
    }
}