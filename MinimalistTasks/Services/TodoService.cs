using Microsoft.EntityFrameworkCore;
using MinimalistTasks.Domain.Context;
using MinimalistTasks.Domain.Dto;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Domain.Model;

namespace MinimalistTasks.Services;

public class TodoService
{
    private readonly MinimalistTasksContext _context;
    private readonly UserService _userService;

    public TodoService(MinimalistTasksContext context, UserService userService)
    {
        _context = context;
        _userService = userService;
    }

    /// <summary>
    /// Returns a list with all Todos
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<TodoDto>> GetAllAsync()
    {
        return await _context.Todos.Select(x => ToDto(x)).ToListAsync();
    }

    /// <summary>
    /// Receives a TodoDto from the controller,
    /// assign the User object into the Todo.User property and inserts into the database
    /// </summary>
    /// <param name="todoDto"></param>
    /// <returns></returns>
    public async Task<TodoDto> InsertAsync(TodoDto todoDto)
    {
        var todo = FromDto(todoDto);
        
        // Get the User
        var userDto = await _userService.GetUserAsync(todo.UserId);
        _context.ChangeTracker.Clear();
        var user = _userService.FromDto(userDto);
        
        // Assign the User to Todo
        todo.User = user;
        todo.TodoId = null;
        _context.Todos.Add(todo);
        
        // Add the Todo to User
        user.Todos.ToList().Add(todo);
        _context.Update(user);
        
        await _context.SaveChangesAsync();
        return ToDto(todo);
    }

    /// <summary>
    /// Convert a Todo to TodoDto
    /// </summary>
    /// <param name="todo">ITodo</param>
    /// <returns>TodoDTO</returns>
    private static TodoDto ToDto(ITodo todo)
    {
        return new TodoDto(todo);
    }

    /// <summary>
    /// Convert a TodoDto to Todo
    /// </summary>
    /// <param name="todoDto">TodoDto</param>
    /// <returns>Todo</returns>
    private static Todo FromDto(TodoDto todoDto)
    {
        return new Todo
        {
            TodoId = todoDto.TodoId, 
            Text = todoDto.Text, 
            CreationDate = todoDto.CreationDate, 
            IsCompleted = todoDto.IsCompleted, 
            UserId = todoDto.UserId
        };
    }
}