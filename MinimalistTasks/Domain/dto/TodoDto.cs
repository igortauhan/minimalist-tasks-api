using MinimalistTasks.Domain.Interface;

namespace MinimalistTasks.Domain.Dto;

public class TodoDto
{
    public int? TodoId { get; set; }
    public string? Text { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }

    public TodoDto()
    {
    }

    public TodoDto(ITodo todo)
    {
        TodoId = todo.TodoId;
        Text = todo.Text;
        CreationDate = todo.CreationDate;
        IsCompleted = todo.IsCompleted;
        UserId = todo.UserId;
    }
}