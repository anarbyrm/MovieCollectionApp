namespace MovieCollectionApi.Models;

public class Movie
{
    public int Id { get; set; }
    public string? ImdbId { get; set; }
    public string Title { get; set; }
    public string? Overview { get; set; }
    public string? PosterPath { get; set; }
}