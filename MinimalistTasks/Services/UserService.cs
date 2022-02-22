using System.Xml;
using Microsoft.EntityFrameworkCore;
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
    /// Returns a list with all Users
    /// </summary>
    /// <returns>List - UserDTO</returns>
    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        return await _context.Users.Select(x => ToDto(x)).ToListAsync();
    }

    /// <summary>
    /// Returns a user if found
    /// </summary>
    /// <param name="userId">int</param>
    /// <returns>UserDTO</returns>
    public async Task<UserDTO> GetUser(int userId)
    {
        var obj = await _context.Users.FindAsync(userId);
        if (obj == null)
        {
            throw new Exception("Not found!");
        }
        return ToDto(obj);
    }

    /// <summary>
    /// Receives a UserDTO from the controller, converts it to User and inserts it into the Database
    /// </summary>
    /// <param name="userDto">UserDTO</param>
    /// <returns>UserDTO</returns>
    public async Task<UserDTO> Insert(UserDTO userDto)
    {
        var user = FromDto(userDto);
        user.UserId = null;
        _context.Add(user);
        await _context.SaveChangesAsync();
        return ToDto(user);
    }

    /// <summary>
    /// Receives the Id and the UserDTO object with new data and transfer it to User in the database
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="userDto">UserDTO</param>
    /// <returns>UserDTO</returns>
    public async Task<UserDTO> Update(int id, UserDTO userDto)
    {
        bool hasUser = await _context.Users.AnyAsync(x => x.UserId == id);
        if (!hasUser)
        {
            throw new Exception("Not found!");
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
    /// <param name="id"></param>
    public async Task Delete(int id)
    {
        var userDto = await GetUser(id);
        _context.ChangeTracker.Clear();
        var user = FromDto(userDto);
        _context.Remove(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Returns a User object with new data
    /// </summary>
    /// <param name="userDto">UserDTO</param>
    /// <returns>User</returns>
    private static User UpdateData(UserDTO userDto)
    {
        return new User
        {
            UserId = userDto.UserId, 
            Name = userDto.Name, 
            Email = userDto.Email
        };
    }

    /// <summary>
    /// Convert a User to UserDTO
    /// </summary>
    /// <param name="user">IUser</param>
    /// <returns>UserDTO</returns>
    private static UserDTO ToDto(IUser user)
    {
        return new UserDTO(user);
    }

    /// <summary>
    /// Convert a UserDTO to User
    /// </summary>
    /// <param name="userDto">UserDTO</param>
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
