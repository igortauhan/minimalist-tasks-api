using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Domain.Model;

namespace MinimalistTasks.Domain.Dto;

public class UserDto
{
    public int? UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public IEnumerable<Todo> Todos { get; set; } = new List<Todo>();

    public UserDto()
    {
    }

    public UserDto(IUser user)
    {
        UserId = user.UserId;
        Name = user.Name;
        Email = user.Email;
    }
}