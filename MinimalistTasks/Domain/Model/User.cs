using MinimalistTasks.Domain.Interface;

namespace MinimalistTasks.Domain.Model;

public class User : IUser
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public IEnumerable<ITodo> Todos { get; set; } = new List<ITodo>();

    public User()
    {
    }

    public User(int userId, string? name, string? email)
    {
        UserId = userId;
        Name = name;
        Email = email;
    }
}