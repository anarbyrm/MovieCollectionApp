using Microsoft.AspNetCore.Mvc;
using MovieCollectionApi.Dto;
using MovieCollectionApi.Services;

namespace MovieCollectionApi.Controllers;

[Route("movies")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly MovieProviderService _movieProviderService;

    public MovieController(MovieProviderService movieProviderService)
    {
        _movieProviderService = movieProviderService;
    }

    [HttpGet("seach")]
    public async Task<IActionResult> SearchMovie([FromQuery] string title)
    {   
        MovieSeachResponseDto? movieData = await _movieProviderService.SearchForMoviesWithTitle(title);
        if (movieData is null)
            return NotFound();
        return Ok(movieData);
    }
}
