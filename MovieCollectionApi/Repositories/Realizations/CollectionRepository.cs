using Microsoft.EntityFrameworkCore;
using MovieCollectionApi.Data;
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Repository;

public class CollectionRepository : ICollectionRepository
{   
    private readonly MovieDbContext _context;

    public CollectionRepository(MovieDbContext context)
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

    public async Task<Collection?> GetOneByIdAsync(int id)
    {
        return await _context.Collections.FirstOrDefaultAsync(collection => collection.Id == id);
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