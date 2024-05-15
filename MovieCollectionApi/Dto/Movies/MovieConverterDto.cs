namespace MovieCollectionApi.Dto;

public class MovieConvertDto
{
    public int id { get; set; }
    public required string imdb_id { get; set; }
    public required string title { get; set; }
    public required string overview { get; set; }
    public required string poster_path { get; set; }
}
