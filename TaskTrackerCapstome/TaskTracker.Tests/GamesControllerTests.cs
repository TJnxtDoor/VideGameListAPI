using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Controllers;
using TaskTracker.Api.Models;

namespace TaskTracker.Tests;

public class GamesControllerTests
{
    [Fact]
    public void GetAllGames_ReturnsOkObjectResult()
    {
        // Arrange
        var controller = new GamesController();

        // Act
        var result = controller.GetAllGames();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetGameById_ValidId_ReturnsOkObjectResult()
    {
        // Arrange
        var controller = new GamesController();

        // Act
        var result = controller.GetGameById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetGameById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var controller = new GamesController();

        // Act
        var result = controller.GetGameById(999);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void CreateGame_ValidGame_ReturnsCreatedAtAction()
    {
        // Arrange
        var controller = new GamesController();
        var newGame = new VideoGame
        {
            Title = "Minecraft",
            Genre = "Sandbox",
            Platform = "PC",
            ReleaseYear = 2011,
            Rating = 9.5
        };

        // Act
        var result = controller.CreateGame(newGame);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void CreateGame_MissingTitle_ReturnsBadRequest()
    {
        // Arrange
        var controller = new GamesController();
        var badGame = new VideoGame { Title = "" };

        // Act
        var result = controller.CreateGame(badGame);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void DeleteGame_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var controller = new GamesController();

        // Act
        var result = controller.DeleteGame(2);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
