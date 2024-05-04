namespace MovieCollectionApi.Models;

public class Collection
{
    public Collection()
    {
        Movies = new List<Movie>();
    }

    public int Id { get; set; }
    public required string Title { get; set; }
    public List<Movie> Movies { get; set; }
}