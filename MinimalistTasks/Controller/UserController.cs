using Microsoft.AspNetCore.Mvc;
using MinimalistTasks.Domain.dto;
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

    [HttpPost]
    public async Task<UserDTO> Insert([FromBody] UserDTO userDto)
    {
        var obj = await _service.Insert(userDto);
        return obj;
    }
}