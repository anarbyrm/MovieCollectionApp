using Microsoft.EntityFrameworkCore;
using MovieCollectionApi.Data;
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Repository;

public class CollectionRepository : ICollectionRepository
{   
    private readonly ApplicationDbContext _context;

    public CollectionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Collection>> GetAllAsync(string? query)
    {
        IQueryable<Collection> collectionQuery = _context.Collections;
        if (query is not null)
            collectionQuery = collectionQuery.Where(collection => collection.Title.Contains(query));
        return await collectionQuery.ToListAsync();
    }

    public async Task<Collection?> GetCollectionByIdAsync(int id, bool includeRelations = false)
    {
        IQueryable<Collection> collections = _context.Collections;
        if (includeRelations)
            collections = collections.Include(c => c.Movies);
        return await collections.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> AddMovieToCollection(Collection collection, Movie movie)
    {
        collection.Movies.Add(movie);
        return await SaveAsync();
    }

    public Movie? GetCollectionMovieById(Collection collection, int movieId)
    {
        Movie? movie = collection.Movies.Find(movie => movie.Id == movieId);
        return movie;
    }

    public async Task<bool> DeleteMovieFromCollection(Collection collection, Movie movie)
    {
        collection.Movies.Remove(movie);
        return await SaveAsync();
    }

    public async Task<bool> CreateAsync(Collection newCollection)
    {
        await _context.AddAsync(newCollection);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(Collection collection)
    {
        _context.Collections.Remove(collection);
        return await SaveAsync();
    }

    public async Task<bool> UpdateAsync(Collection updatedCollection)
    {
        _context.Collections.Update(updatedCollection);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }
}
