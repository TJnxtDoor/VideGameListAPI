using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Models;
using System;
using System.Linq;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private static readonly string[] SampleTitles = new[]
    {
        "Grand Theft Auto V",
        "The Legend of Zelda: Breath of the Wild",
        "Need For Speed: Heat",
        "Forza Horizon 5",
        "Runebound",
        "Cyber Frontier",
        "Temple of Echoes",
        "Iron Frontier",
        "Aether Quest",
        "Shadow Harbor",
        "Galaxy Outlaws"
    };

    private static readonly string[] SampleGenres = new[]
    {
        "Adventure",
        "RPG",
        "Platformer",
        "Action",
        "Puzzle",
        "Simulation",
        "Strategy",
        "Shooter",
        "Horror",
        "Racing"
    };

    private static readonly string[] SamplePlatforms = new[]
    {
        "PC",
        "Xbox",
        "PlayStation",
        "Switch",
        "Mobile"
    };

    private static readonly Random RandomGenerator = new();

    private static List<VideoGame> games = CreateInitialGames();

    private static List<VideoGame> CreateInitialGames()
    {
        return Enumerable.Range(1, SampleTitles.Length)
            .Select(id => new VideoGame
            {
                Id = id,
                Title = SampleTitles[id - 1],
                Genre = SampleGenres[RandomGenerator.Next(SampleGenres.Length)],
                Platform = SamplePlatforms[RandomGenerator.Next(SamplePlatforms.Length)],
                ReleaseYear = RandomGenerator.Next(2008, DateTime.Now.Year + 1),
                Rating = Math.Round(6.0 + RandomGenerator.NextDouble() * 4.0, 1)
            })
            .ToList();
    }

    [HttpGet]
    public IActionResult GetAllGames()
    {
        return Ok(games);
    }

    [HttpGet("{id}")]
    public IActionResult GetGameById(int id)
    {
        var game = games.FirstOrDefault(g => g.Id == id);
        if (game == null)
        {
            return NotFound($"Game with ID {id} was not found.");
        }
        return Ok(game);
    }

    [HttpPost]
    public IActionResult CreateGame(VideoGame newGame)
    {
        if (string.IsNullOrEmpty(newGame.Title))
        {
            return BadRequest("Title is required.");
        }

        int nextId = 1;
        if (games.Count > 0)
        {
            nextId = games.Max(g => g.Id) + 1;
        }

        newGame.Id = nextId;
        games.Add(newGame);

        return CreatedAtAction(nameof(GetGameById), new { id = newGame.Id }, newGame);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGame(int id, VideoGame updatedGame)
    {
        var existingGame = games.FirstOrDefault(g => g.Id == id);
        if (existingGame == null)
        {
            return NotFound($"Game with ID {id} was not found.");
        }

        existingGame.Title = updatedGame.Title;
        existingGame.Genre = updatedGame.Genre;
        existingGame.Platform = updatedGame.Platform;
        existingGame.ReleaseYear = updatedGame.ReleaseYear;
        existingGame.Rating = updatedGame.Rating;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGame(int id)
    {
        var gameToDelete = games.FirstOrDefault(g => g.Id == id);
        if (gameToDelete == null)
        {
            return NotFound($"Game with ID {id} was not found.");
        }

        games.Remove(gameToDelete);
        return NoContent();
    }
}
