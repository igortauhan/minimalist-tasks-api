using MinimalistTasks.Domain.Dto;

namespace MinimalistTasks.Services;

public interface ITodoService
{
    /// <summary>
    /// Returns a list with all Todos
    /// </summary>
    /// <returns>List - TodoDto</returns>
    Task<IEnumerable<TodoDto>> GetAllAsync();

    /// <summary>
    /// Returns a Todo if found
    /// </summary>
    /// <param name="todoId">int</param>
    /// <returns>TodoDto</returns>
    /// <exception cref="Exception"></exception>
    Task<TodoDto> GetTodoAsync(int todoId);

    /// <summary>
    /// Receives a TodoDto from the controller,
    /// assign the User object into the Todo.User property and inserts into the database
    /// </summary>
    /// <param name="todoDto">TodoDto</param>
    /// <returns>TodoDto</returns>
    Task<TodoDto> InsertAsync(TodoDto todoDto);

    /// <summary>
    /// Update the Todo data
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="newTodoDto">NewTodoDto</param>
    /// <returns>TodoDto</returns>
    Task<TodoDto> UpdateAsync(int id, NewTodoDto newTodoDto);

    /// <summary>
    /// Receives the Todo todoId and delete it to the database
    /// </summary>
    /// <param name="id">int</param>
    Task DeleteAsync(int id);
}