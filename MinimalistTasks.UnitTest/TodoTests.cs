using System;
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
public class TodoTests
{
    private ILogger<ITodo> _logger;
    private Mock<ITodoService> _todoService;
    private TodoController _controller;

    [SetUp]
    public void Setup()
    {
        _todoService = new Mock<ITodoService>();
        _controller = new TodoController(_logger, _todoService.Object);
    }

    [Test]
    public async Task GetTodoAsync_WhenCalled_ShouldReturnATodoDto()
    {
        // Arrange
        _todoService.Setup(x => x.GetTodoAsync(1)).ReturnsAsync(new TodoDto
        {
            TodoId = 1,
        });

        // Act
        var result = await _controller.GetTodo(1);

        // Assert
        Assert.That(result.TodoId, Is.EqualTo(1));
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ShouldReturnAListOfTodoDto()
    {
        // Arrange
        var todos = new List<TodoDto>
        {
            new TodoDto(),
            new TodoDto(),
            new TodoDto()
        };
        _todoService.Setup(x => x.GetAllAsync()).ReturnsAsync(todos);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task InsertAsync_WhenCalled_ShouldInsertATodoAndReturnATodoDto()
    {
        // Arrange
        var todo = new TodoDto
        {
            TodoId = 1
        };
        _todoService.Setup(x => x.InsertAsync(todo)).ReturnsAsync(new TodoDto
        {
            TodoId = 1
        });

        // Act
        var result = await _controller.Insert(todo);

        // Assert
        Assert.That(result.TodoId, Is.EqualTo(1));
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_ShouldUpdateTheTodoDataAndReturnATodoDto()
    {
        // Arrange
        var newTodoDto = new NewTodoDto
        {
            Text = "Now is task two",
            IsCompleted = false
        };
        _todoService.Setup(x => x.UpdateAsync(1, newTodoDto)).ReturnsAsync(new TodoDto
        {
            TodoId = 1,
            Text = newTodoDto.Text,
            CreationDate = DateTime.Now,
            IsCompleted = newTodoDto.IsCompleted,
            UserId = 1
        });

        // Act
        var result = await _controller.Update(1, newTodoDto);

        // Assert
        Assert.That(result.TodoId, Is.EqualTo(1));
    }
}