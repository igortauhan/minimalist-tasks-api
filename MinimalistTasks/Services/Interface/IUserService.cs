using MinimalistTasks.Domain.Dto;

namespace MinimalistTasks.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetUserAsync(int userId);
    Task<UserDto> InsertAsync(UserDto userDto);
    Task<UserDto> UpdateAsync(int id, UserDto userDto);
    Task DeleteAsync(int id);
}