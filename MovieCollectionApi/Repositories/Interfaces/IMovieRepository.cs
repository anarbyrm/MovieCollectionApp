using MovieCollectionApi.Models;

namespace MovieCollectionApi.Repository;

public interface IMovieRepository
{
    Task<bool> CheckMovieExistsAsync(int movieId);
    Task<Movie?> FetchMovieById(int movieId);
}
