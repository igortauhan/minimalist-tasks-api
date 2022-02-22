using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Domain.Model;

namespace MinimalistTasks.Domain.dto;

public class UserDTO
{
    public int? UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public IEnumerable<Todo> Todos { get; set; } = new List<Todo>();

    public UserDTO()
    {
    }

    public UserDTO(IUser user)
    {
        UserId = user.UserId;
        Name = user.Name;
        Email = user.Email;
    }
}