namespace MovieCollectionApi.Models;

public class Movie
{
    public Movie()
    {
        Collections = new List<Collection>();
    }

    public int Id { get; set; }
    public required string ImdbId { get; set; }
    public required string Title { get; set; }
    public required string Overview { get; set; }
    public required string PosterPath { get; set; }
    public List<Collection> Collections { get; set; }
}