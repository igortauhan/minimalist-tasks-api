namespace MinimalistTasks.Domain.Interface;

public interface ITodo
{
    public int TodoId { get; set; }
    public string? Text { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsCompleted { get; set; }
}