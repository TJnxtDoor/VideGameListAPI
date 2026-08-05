using System.Text;
using System.Text.Json;
using TaskTracker.Client.Models;

namespace TaskTracker.Client;

class Program
{
    private static HttpClient client = new HttpClient();
    private static string baseUrl = "http://localhost:5000/api/games";

    static async Task Main(string[] args)
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.WriteLine("\n=== VIDEO GAME TRACKER ===");
            Console.WriteLine("1. Show All Video Games");
            Console.WriteLine("2. Get Video Game By ID");
            Console.WriteLine("3. Add New Video Game");
            Console.WriteLine("4. Update Existing Video Game");
            Console.WriteLine("5. Delete Video Game");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option (1-6): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                await GetAllGames();
            }
            else if (choice == "2")
            {
                await GetGameById();
            }
            else if (choice == "3")
            {
                await AddGame();
            }
            else if (choice == "4")
            {
                await UpdateGame();
            }
            else if (choice == "5")
            {
                await DeleteGame();
            }
            else if (choice == "6")
            {
                keepRunning = false;
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }

    static async Task GetAllGames()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<VideoGame> games = JsonSerializer.Deserialize<List<VideoGame>>(jsonString, options);

                Console.WriteLine("--- VIDEO GAME LIST ---");
                if (games != null && games.Count > 0)
                {
                    foreach (var game in games)
                    {
                        Console.WriteLine($"ID: {game.Id} | {game.Title} | Genre: {game.Genre} | Platform: {game.Platform} | Year: {game.ReleaseYear} | Rating: {game.Rating}/10");
                    }
                }
                else
                {
                    Console.WriteLine("No video games found.");
                }
            }
            else
            {
                Console.WriteLine("Error fetching games. Status: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to API. Make sure the API project is running!");
            Console.WriteLine("Message: " + ex.Message);
        }
    }

    static async Task GetGameById()
    {
        Console.Write("Enter Video Game ID: ");
        string idInput = Console.ReadLine();

        if (int.TryParse(idInput, out int id))
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                VideoGame game = JsonSerializer.Deserialize<VideoGame>(jsonString, options);

                if (game != null)
                {
                    Console.WriteLine($"\n--- GAME {game.Id} DETAILS ---");
                    Console.WriteLine("Title: " + game.Title);
                    Console.WriteLine("Genre: " + game.Genre);
                    Console.WriteLine("Platform: " + game.Platform);
                    Console.WriteLine("Release Year: " + game.ReleaseYear);
                    Console.WriteLine("Rating: " + game.Rating + "/10");
                }
            }
            else
            {
                Console.WriteLine("Video game not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID number.");
        }
    }

    static async Task AddGame()
    {
        Console.Write("Enter Title: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Enter Genre: ");
        string genre = Console.ReadLine() ?? "";

        Console.Write("Enter Platform (e.g. PC, Switch, PS5): ");
        string platform = Console.ReadLine() ?? "";

        Console.Write("Enter Release Year: ");
        int.TryParse(Console.ReadLine(), out int releaseYear);

        Console.Write("Enter Rating (0.0 - 10.0): ");
        double.TryParse(Console.ReadLine(), out double rating);

        VideoGame newGame = new VideoGame
        {
            Title = title,
            Genre = genre,
            Platform = platform,
            ReleaseYear = releaseYear,
            Rating = rating
        };

        string json = JsonSerializer.Serialize(newGame);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(baseUrl, content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Video game added successfully!");
        }
        else
        {
            Console.WriteLine("Failed to add video game.");
        }
    }

    static async Task UpdateGame()
    {
        Console.Write("Enter Video Game ID to update: ");
        string idInput = Console.ReadLine();

        if (int.TryParse(idInput, out int id))
        {
            Console.Write("Enter New Title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Enter New Genre: ");
            string genre = Console.ReadLine() ?? "";

            Console.Write("Enter New Platform: ");
            string platform = Console.ReadLine() ?? "";

            Console.Write("Enter New Release Year: ");
            int.TryParse(Console.ReadLine(), out int releaseYear);

            Console.Write("Enter New Rating (0.0 - 10.0): ");
            double.TryParse(Console.ReadLine(), out double rating);

            VideoGame updatedGame = new VideoGame
            {
                Id = id,
                Title = title,
                Genre = genre,
                Platform = platform,
                ReleaseYear = releaseYear,
                Rating = rating
            };

            string json = JsonSerializer.Serialize(updatedGame);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(baseUrl + "/" + id, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Video game updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update video game.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID number.");
        }
    }

    static async Task DeleteGame()
    {
        Console.Write("Enter Video Game ID to delete: ");
        string idInput = Console.ReadLine();

        if (int.TryParse(idInput, out int id))
        {
            HttpResponseMessage response = await client.DeleteAsync(baseUrl + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Video game deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete video game.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID number.");
        }
    }
}
