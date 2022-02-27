using System.Collections.Generic;
using System.Linq;
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

    [Test]
    public async Task GetAllAsync_WhenCalled_ShouldReturnAListOfUserDto()
    {
        // Arrange
        _userService.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<UserDto>
        {
            new UserDto(),
            new UserDto(),
            new UserDto()
        });

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task InsertAsync_WhenCalled_ShouldInsertAUserAndReturnAUserDto()
    {
        // Arrange
        var user = new UserDto
        {
            UserId = 1,
            Name = "Rob",
            Email = "rob@rob.com"
        };
        _userService.Setup(x => x.InsertAsync(user)).ReturnsAsync(new UserDto
        {
            UserId = 1,
            Name = "Rob",
            Email = "rob@rob.com"
        });

        // Act
        var result = await _controller.Insert(user);

        // Assert
        Assert.That(result.UserId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("rob").IgnoreCase);
        Assert.That(result.Email, Is.EqualTo("rob@rob.com").IgnoreCase);
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_ShouldUpdateTheUserDataAndReturnAUserDto()
    {
        // Arrange
        var oldUser = new UserDto
        {
            UserId = 1,
            Name = "Rob",
            Email = "rob@rob.com"
        };
        var newUser = new UserDto
        {
            UserId = 1,
            Name = "Rob Less",
            Email = "robless@rob.com"
        };
        _userService.Setup(x => x.UpdateAsync(1, oldUser)).ReturnsAsync(newUser);

        // Act
        var result = await _controller.Update(1, oldUser);

        // Assert
        Assert.That(result.UserId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Rob Less").IgnoreCase);
        Assert.That(result.Email, Is.EqualTo("robless@rob.com").IgnoreCase);
    }
}