using System.ComponentModel.DataAnnotations;

namespace MinimalistTasks.Domain.Dto;

public class NewTodoDto
{
    [Required]
    [StringLength(100)]
    public string? Text { get; set; }
    
    [Required]
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