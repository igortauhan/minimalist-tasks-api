using Microsoft.EntityFrameworkCore;
using MinimalistTasks.Domain.Context;
using MinimalistTasks.Domain.Dto;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Domain.Model;
using MinimalistTasks.Exceptions;

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
    /// <returns>List - TodoDto</returns>
    public async Task<IEnumerable<TodoDto>> GetAllAsync()
    {
        return await _context.Todos.Select(x => ToDto(x)).ToListAsync();
    }

    /// <summary>
    /// Returns a Todo if found
    /// </summary>
    /// <param name="todoId">int</param>
    /// <returns>TodoDto</returns>
    /// <exception cref="Exception"></exception>
    public async Task<TodoDto> GetTodoAsync(int todoId)
    {
        var todo = await _context.Todos.FindAsync(todoId);
        if (todo == null)
        {
            throw new ObjectNotFoundException("Todo not found! Id: " + todoId);
        }
        return ToDto(todo);
    }

    /// <summary>
    /// Receives a TodoDto from the controller,
    /// assign the User object into the Todo.User property and inserts into the database
    /// </summary>
    /// <param name="todoDto">TodoDto</param>
    /// <returns>TodoDto</returns>
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
    /// Update the Todo data
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="newTodoDto">NewTodoDto</param>
    /// <returns>TodoDto</returns>
    public async Task<TodoDto> UpdateAsync(int id, NewTodoDto newTodoDto)
    {
        var todoDto = await GetTodoAsync(id);
        var todo = FromDto(todoDto);
        _context.ChangeTracker.Clear();
        
        UpdateData(newTodoDto, todo);
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
        return ToDto(todo);
    }

    /// <summary>
    /// Receives the Todo todoId and delete it to the database
    /// </summary>
    /// <param name="id">int</param>
    public async Task DeleteAsync(int id)
    {
        var todoDto = await GetTodoAsync(id);
        _context.ChangeTracker.Clear();
        var todo = FromDto(todoDto);
        _context.Remove(todo);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Copy the new data to old Todo
    /// </summary>
    /// <param name="newTodoDto">NewTodoDto</param>
    /// <param name="todo">ITodo</param>
    private static void UpdateData(NewTodoDto newTodoDto, ITodo todo)
    {
        todo.Text = newTodoDto.Text;
        todo.IsCompleted = newTodoDto.IsCompleted;
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