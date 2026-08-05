namespace TaskTracker.Client.Models;

public class VideoGame
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public string Platform { get; set; } = "";
    public int ReleaseYear { get; set; }
    public double Rating { get; set; }
}
