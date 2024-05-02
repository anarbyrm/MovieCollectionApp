using System.Collections;
using System.Text.Json;
using System.Web;
using MovieCollectionApi.Dto;

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

    public async Task<MovieSeachResponseDto?> SearchForMoviesWithTitle(string title)
    {   
        var uriBuilder = new UriBuilder(_baseUrl + "/search/movie");

        // prepare query string
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["query"] = title;
        uriBuilder.Query = query.ToString();

        string absoluteUri = uriBuilder.ToString();
        var response = await _httpClient.GetAsync(absoluteUri);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MovieSeachResponseDto>(data);
        }
        return null;
    }

    // fetch movie detail based on movie_id

    // fetch recommended movies

}
