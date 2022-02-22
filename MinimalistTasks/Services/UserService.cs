using MinimalistTasks.Domain.Context;
using MinimalistTasks.Domain.dto;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Domain.Model;

namespace MinimalistTasks.Services;

public class UserService
{
    private readonly MinimalistTasksContext _context;

    public UserService(MinimalistTasksContext context)
    {
        _context = context;
    }

    public async Task<UserDTO> Insert(UserDTO userDto)
    {
        var user = FromDto(userDto);
        _context.Add(user);
        await _context.SaveChangesAsync();
        return ToDto(user);
    }

    private static UserDTO ToDto(IUser user)
    {
        return new UserDTO(user);
    }

    private static User FromDto(UserDTO userDto)
    {
        return new User
        {
            UserId = userDto.UserId, 
            Name = userDto.Name, 
            Email = userDto.Email
        };
    }
}