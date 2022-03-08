using Microsoft.AspNetCore.Authorization;
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
    private readonly IUserService _service;

    public UserController(ILogger<IUser> logger, IUserService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    [Route("login")]
    public async Task<dynamic> Authenticate([FromBody] UserDto userDto)
    {
        var user = await _service.GetUserAsync(userDto.Email, userDto.Password);
        var token = TokenService.GenerateToken(user);
        user.Password = "";
        return new
        {
            user = user,
            token = token
        };
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
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
    [Authorize]
    public async Task<UserDto> Update(int id, [FromBody] UserDto userDto)
    {
        var obj = await _service.UpdateAsync(id, userDto);
        return obj;
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}
