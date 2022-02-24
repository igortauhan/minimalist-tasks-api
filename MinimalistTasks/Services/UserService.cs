using Microsoft.EntityFrameworkCore;
using MinimalistTasks.Domain.Context;
using MinimalistTasks.Domain.Dto;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Domain.Model;
using MinimalistTasks.Exceptions;

namespace MinimalistTasks.Services;

public class UserService
{
    private readonly MinimalistTasksContext _context;

    public UserService(MinimalistTasksContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns a list with all Users
    /// </summary>
    /// <returns>List - UserDto</returns>
    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        return await _context.Users.Select(x => ToDto(x)).ToListAsync();
    }

    /// <summary>
    /// Returns a user if found
    /// </summary>
    /// <param name="userId">int</param>
    /// <returns>UserDto</returns>
    public async Task<UserDto> GetUserAsync(int userId)
    {
        var obj = await _context.Users.FindAsync(userId);
        if (obj == null)
        {
            throw new ObjectNotFoundException("User not found! Id: " + userId);
        }

        return ToDto(obj);
    }

    /// <summary>
    /// Receives a UserDto from the controller, converts it to User and inserts it into the Database
    /// </summary>
    /// <param name="userDto">UserDto</param>
    /// <returns>UserDto</returns>
    public async Task<UserDto> InsertAsync(UserDto userDto)
    {
        var user = FromDto(userDto);
        user.UserId = null;
        _context.Add(user);
        await _context.SaveChangesAsync();
        return ToDto(user);
    }

    /// <summary>
    /// Receives the Id and the UserDto object with new data and transfer it to User in the database
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="userDto">UserDto</param>
    /// <returns>UserDto</returns>
    public async Task<UserDto> UpdateAsync(int id, UserDto userDto)
    {
        bool hasUser = await _context.Users.AnyAsync(x => x.UserId == id);
        if (!hasUser)
        {
            throw new ObjectNotFoundException("User not found! Id: " + id);
        }

        userDto.UserId = id;
        var newUser = UpdateData(userDto);

        _context.Update(newUser);
        await _context.SaveChangesAsync();

        return ToDto(newUser);
    }

    /// <summary>
    /// Receives the User id and delete it from the database
    /// </summary>
    /// <param name="id">int</param>
    public async Task DeleteAsync(int id)
    {
        var userDto = await GetUserAsync(id);
        _context.ChangeTracker.Clear();
        var user = FromDto(userDto);
        _context.Remove(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Returns a User object with new data
    /// </summary>
    /// <param name="userDto">UserDto</param>
    /// <returns>User</returns>
    private static User UpdateData(UserDto userDto)
    {
        return new User
        {
            UserId = userDto.UserId,
            Name = userDto.Name,
            Email = userDto.Email
        };
    }

    /// <summary>
    /// Convert a User to UserDto
    /// </summary>
    /// <param name="user">IUser</param>
    /// <returns>UserDto</returns>
    private static UserDto ToDto(IUser user)
    {
        return new UserDto(user);
    }

    /// <summary>
    /// Convert a UserDto to User
    /// </summary>
    /// <param name="userDto">UserDto</param>
    /// <returns>User</returns>
    public User FromDto(UserDto userDto)
    {
        return new User
        {
            UserId = userDto.UserId,
            Name = userDto.Name,
            Email = userDto.Email
        };
    }
}