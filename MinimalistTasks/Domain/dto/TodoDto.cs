using System.ComponentModel.DataAnnotations;
using MinimalistTasks.Domain.Interface;

namespace MinimalistTasks.Domain.Dto;

public class TodoDto
{
    public int? TodoId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? Text { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsCompleted { get; set; }
    
    [Required]
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