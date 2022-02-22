using MinimalistTasks.Domain.Interface;

namespace MinimalistTasks.Domain.Model;

public class Todo : ITodo
{
    public int TodoId { get; set; }
    public string? Text { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public Todo()
    {
    }

    public Todo(int todoId, string? text, DateTime creationDate, bool isCompleted, int userId, User user)
    {
        TodoId = todoId;
        Text = text;
        CreationDate = creationDate;
        IsCompleted = isCompleted;
        UserId = userId;
        User = user;
    }
}