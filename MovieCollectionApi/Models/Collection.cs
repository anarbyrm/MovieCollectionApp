namespace MovieCollectionApi.Models;

public class Collection
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required List<Movie> Movies { get; set; }
}