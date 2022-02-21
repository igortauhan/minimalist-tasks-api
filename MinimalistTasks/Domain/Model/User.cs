using MinimalistTasks.Domain.Interface;

namespace MinimalistTasks.Domain.Model;

public class User : IUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IEnumerable<ITodo> Todos { get; set; } = new List<ITodo>();

    public User(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}