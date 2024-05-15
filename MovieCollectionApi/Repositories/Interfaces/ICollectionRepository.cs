
using MovieCollectionApi.Dto;
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Repository;

public interface ICollectionRepository
{
    Task<List<Collection>> GetAllAsync(string query);
    Task<Collection?> GetCollectionByIdAsync(int id, bool includeRelations);
    Task<bool> CreateAsync(Collection newCollection);
    Task<bool> DeleteAsync(Collection collection);
    Task<bool> UpdateAsync(Collection updatedCollection);
    Task<bool> SaveAsync();
    Task<bool> AddMovieToCollection(Collection collection, Movie movie);
    Movie? GetCollectionMovieById(Collection collection, int movieId);
    Task<bool> DeleteMovieFromCollection(Collection collection, Movie movie);
}
