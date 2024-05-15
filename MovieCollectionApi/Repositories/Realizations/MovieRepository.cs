using Microsoft.EntityFrameworkCore;
using MovieCollectionApi.Data;
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly MovieDbContext _dbContext;

    public MovieRepository(MovieDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CheckMovieExistsAsync(int movieId)
    {
        return await _dbContext.Movies.AnyAsync(movie => movie.Id == movieId);
    }

    public async Task<Movie?> FetchMovieById(int movieId)
    {
        return await _dbContext.Movies.FindAsync(movieId);
    }
}