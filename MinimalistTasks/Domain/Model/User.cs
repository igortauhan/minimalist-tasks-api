using MinimalistTasks.Domain.Interface;

namespace MinimalistTasks.Domain.Model;

public class User : IUser
{
    public int? UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public IEnumerable<Todo> Todos { get; set; } = new List<Todo>();

    public User()
    {
    }

    public User(int userId, string? name, string? email, string? password)
    {
        UserId = userId;
        Name = name;
        Email = email;
        Password = password;
    }
}