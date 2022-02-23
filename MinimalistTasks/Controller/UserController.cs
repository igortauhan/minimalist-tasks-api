using Microsoft.AspNetCore.Mvc;
using MinimalistTasks.Domain.Dto;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Services;

namespace MinimalistTasks.Controller;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<IUser> _logger;
    private readonly UserService _service;

    public UserController(ILogger<IUser> logger, UserService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetAll()
    {
        var obj = await _service.GetAllAsync();
        return obj;
    }

    [HttpGet("{id:int}")]
    public async Task<UserDto> GetUser(int id)
    {
        var obj = await _service.GetUserAsync(id);
        return obj;
    }

    [HttpPost]
    public async Task<UserDto> Insert([FromBody] UserDto userDto)
    {
        var obj = await _service.InsertAsync(userDto);
        return obj;
    }

    [HttpPut("{id:int}")]
    public async Task<UserDto> Update(int id, [FromBody] UserDto userDto)
    {
        var obj = await _service.UpdateAsync(id, userDto);
        return obj;
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}
