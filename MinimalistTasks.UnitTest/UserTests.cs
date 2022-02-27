using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MinimalistTasks.Controller;
using MinimalistTasks.Domain.Dto;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Services;
using Moq;
using NUnit.Framework;

namespace MinimalistTasks.UnitTest;

[TestFixture]
public class UserTests
{
    private ILogger<IUser> _logger;
    private Mock<IUserService> _userService;
    private UserController _controller;

    [SetUp]
    public void Setup()
    {
        _userService = new Mock<IUserService>();
        _controller = new UserController(_logger, _userService.Object);
    }

    [Test]
    public async Task GetUserAsync_WhenCalled_ShouldReturnAUserDto()
    {
        // Arrange
        _userService.Setup(x => x.GetUserAsync(1)).ReturnsAsync(new UserDto
        {
            UserId = 1,
            Name = "Rob",
            Email = "rob@rob.com"
        });
        
        // Act
        var result = await _controller.GetUser(1);

        // Assert
        Assert.That(result.UserId, Is.EqualTo(1));
        Assert.That(result.Name, Does.Contain("rob").IgnoreCase);
        Assert.That(result.Email, Is.EqualTo("rob@rob.com"));
    }
}