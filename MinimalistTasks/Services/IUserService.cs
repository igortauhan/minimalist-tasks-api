using MinimalistTasks.Domain.Dto;

namespace MinimalistTasks.Services;

public interface IUserService
{
    /// <summary>
    /// Returns a list with all Users
    /// </summary>
    /// <returns>List - UserDto</returns>
    Task<IEnumerable<UserDto>> GetAllAsync();

    /// <summary>
    /// Returns a user if found
    /// </summary>
    /// <param name="userId">int</param>
    /// <returns>UserDto</returns>
    Task<UserDto> GetUserAsync(int userId);

    /// <summary>
    /// Receives a UserDto from the controller, converts it to User and inserts it into the Database
    /// </summary>
    /// <param name="userDto">UserDto</param>
    /// <returns>UserDto</returns>
    Task<UserDto> InsertAsync(UserDto userDto);

    /// <summary>
    /// Receives the Id and the UserDto object with new data and transfer it to User in the database
    /// </summary>
    /// <param name="id">int</param>
    /// <param name="userDto">UserDto</param>
    /// <returns>UserDto</returns>
    Task<UserDto> UpdateAsync(int id, UserDto userDto);

    /// <summary>
    /// Receives the User id and delete it from the database
    /// </summary>
    /// <param name="id">int</param>
    Task DeleteAsync(int id);
}