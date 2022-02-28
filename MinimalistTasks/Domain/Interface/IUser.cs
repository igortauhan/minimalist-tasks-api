using MinimalistTasks.Domain.Model;

namespace MinimalistTasks.Domain.Interface;

public interface IUser
{
    public int? UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public IEnumerable<Todo> Todos { get; set; }
}