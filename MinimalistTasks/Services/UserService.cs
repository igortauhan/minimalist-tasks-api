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

    /// <summary>
    /// Receives a UserDTO from the controller, converts it to User and inserts it into the Database
    /// </summary>
    /// <param name="userDto">userDto</param>
    /// <returns>UserDTO</returns>
    public async Task<UserDTO> Insert(UserDTO userDto)
    {
        var user = FromDto(userDto);
        _context.Add(user);
        await _context.SaveChangesAsync();
        return ToDto(user);
    }

    /// <summary>
    /// Convert a User to UserDTO
    /// </summary>
    /// <param name="user">user</param>
    /// <returns>UserDTO</returns>
    private static UserDTO ToDto(IUser user)
    {
        return new UserDTO(user);
    }

    /// <summary>
    /// Convert a UserDTO to User
    /// </summary>
    /// <param name="userDto">userDto</param>
    /// <returns>User</returns>
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
