namespace MovieCollectionApi.Dto;

public class MovieSeachResponseDto
{
    public required MovieResult[] results { get; set; }
}

public class MovieResult
{
    public int id { get; set; }
    public bool adult { get; set; }
    public required string backdrop_path { get; set; }
    public required int[] genre_ids { get; set; }
    public required string original_language { get; set; }
    public required string original_title { get; set; }
    public required string overview { get; set; }
    public float popularity { get; set; }
    public required string poster_path { get; set; }
    public required string release_date { get; set; }
    public required string title { get; set; }
    public bool video { get; set; }
    public float vote_average { get; set; }
    public int vote_count { get; set; }
}
