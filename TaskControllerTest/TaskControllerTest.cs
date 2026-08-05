using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Controllers;
using TaskTracker.Api.Models;

namespace TaskTracker.Tests;

public class TasksControllerTests
{
    [Fact]
    public void GetAllTasks_ReturnsOkObjectResult()
    {
        // Arrange
        var controller = new TasksController();

        // Act
        var result = controller.GetAllTasks();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetTaskById_ValidId_ReturnsOkObjectResult()
    {
        // Arrange
        var controller = new TasksController();

        // Act
        var result = controller.GetTaskById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetTaskById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var controller = new TasksController();

        // Act
        var result = controller.GetTaskById(999);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void CreateTask_ValidTask_ReturnsCreatedAtAction()
    {
        // Arrange
        var controller = new TasksController();
        var newTask = new TaskItem
        {
            Title = "Test Task",
            Description = "Testing the POST endpoint",
            Priority = "High",
            IsCompleted = false
        };

        // Act
        var result = controller.CreateTask(newTask);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void CreateTask_MissingTitle_ReturnsBadRequest()
    {
        // Arrange
        var controller = new TasksController();
        var badTask = new TaskItem { Title = "" };

        // Act
        var result = controller.CreateTask(badTask);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void DeleteTask_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var controller = new TasksController();

        // Act
        var result = controller.DeleteTask(2);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}