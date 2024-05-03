namespace MovieCollectionApi.Models;

public class Movie
{
    public int Id { get; set; }
    public required string ImdbId { get; set; }
    public required string Title { get; set; }
    public required string Overview { get; set; }
    public required string PosterPath { get; set; }
    public required List<Collection> Collections { get; set; }
}