namespace MinimalistTasks.Domain.Dto;

public class NewTodoDto
{
    public string? Text { get; set; }
    public bool IsCompleted { get; set; }

    public NewTodoDto()
    {
    }

    public NewTodoDto(string? text, bool isCompleted)
    {
        Text = text;
        IsCompleted = isCompleted;
    }
}