using MinimalistTasks.Domain.Dto;

namespace MinimalistTasks.Services.Interface;

public interface ITodoService
{
    Task<IEnumerable<TodoDto>> GetAllAsync();
    Task<TodoDto> GetTodoAsync(int todoId);
    Task<TodoDto> InsertAsync(TodoDto todoDto);
    Task<TodoDto> UpdateAsync(int id, NewTodoDto newTodoDto);
    Task DeleteAsync(int id);
}