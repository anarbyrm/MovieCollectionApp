using Microsoft.AspNetCore.Mvc;
using MovieCollectionApi.Services;

namespace MovieCollectionApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieProviderService _movieProviderService;

    public MoviesController(MovieProviderService movieProviderService)
    {
        _movieProviderService = movieProviderService;
    }

    [HttpGet("seach")]
    public async Task<IActionResult> SearchMovie([FromQuery] string title)
    {   
        dynamic? movies = await _movieProviderService.SearchForMoviesWithTitle(title);
        if (movies is null)
            return NotFound();
        return Ok(movies);
    }

    [HttpGet("{movieId}")]
    public async Task<IActionResult> GetMovie(int movieId)
    {
        dynamic? movie = await _movieProviderService.FetchMovieDetailById(movieId);
        if (movie is null)
            return NotFound();
        return Ok(movie);
    }

    [HttpGet("{movieId}/recommendations")]
    public async Task<IActionResult> GetMovieRecommendation(int movieId)
    {
        dynamic? movie = await _movieProviderService.FetchRecommendedMoviesBasedOnMovieId(movieId);
        if (movie is null)
            return NotFound();
        return Ok(movie);
    }
}
