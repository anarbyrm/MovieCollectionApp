using System.Text.Json;
using System.Web;

namespace MovieCollectionApi.Services;

public class MovieProviderService
{   
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://api.themoviedb.org/3";

    public MovieProviderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // todo: add Configuration service for env variables
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJjYjQzMjFlZGQwZDk5YWIxOGZkYjJiNDdjMTU5ZDk2MSIsInN1YiI6IjY2MzNiMzU1YWY0MzI0MDEyYjU0MjI5NiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.-ADDYUC5x8qBO0dV7vRLqXGAYz3nYP27zIFYuQoMalE");
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    private string PrepareAbsoluteUri(string path, string? queryParam)
    {
        string url = _baseUrl + path;
        var uriBuilder = new UriBuilder(url);

        // prepare query string
        if (queryParam is not null)
        {
            // e.g: https://baseUrl?query=example
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["query"] = queryParam;
            uriBuilder.Query = query.ToString();
        }

        return uriBuilder.ToString();
    }

    private async Task<T?> SendGetRequest<T>(string endpointPath, string? queryParam) where T: class
    {

        string absoluteUri = PrepareAbsoluteUri(endpointPath, queryParam);
        var response = await _httpClient.GetAsync(absoluteUri);

        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();            
            return JsonSerializer.Deserialize<dynamic>(data);
        }
        return null;
    }

    public async Task<dynamic?> SearchForMoviesWithTitle(string title)
    {   
        return await SendGetRequest<dynamic>(
            endpointPath: "/search/movie",
            queryParam: $"{title}"
        );
    }

    public async Task<dynamic?> FetchMovieDetailById(int movieId)
    {
        return await SendGetRequest<dynamic>(
            endpointPath: $"/movie/{movieId}",
            queryParam: null
        );
    }

    public async Task<dynamic?> FetchRecommendedMoviesBasedOnMovieId(int movieId)
    {
        return await SendGetRequest<dynamic>(
            endpointPath: $"/movie/{movieId}/recommendations",
            queryParam: null
        );
    }
}
