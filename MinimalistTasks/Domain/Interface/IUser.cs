namespace MinimalistTasks.Domain.Interface;

public interface IUser
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public IEnumerable<ITodo> Todos { get; set; }
}