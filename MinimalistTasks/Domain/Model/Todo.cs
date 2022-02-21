using MinimalistTasks.Domain.Interface;

namespace MinimalistTasks.Domain.Model;

public class Todo : ITodo
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsCompleted { get; set; }

    public Todo(int id, string text, DateTime creationDate, bool isCompleted)
    {
        Id = id;
        Text = text;
        CreationDate = creationDate;
        IsCompleted = isCompleted;
    }
}