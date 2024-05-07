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

    public List<Collection> GetAll()
    {
        return _context.Collections.ToList();
    }

    public Collection? GetOneById(int Id)
    {
        return _context.Collections.FirstOrDefault(collection => collection.Id == Id);
    }

    public Collection? GetOneByTitle(string Title)
    {
        return _context.Collections.FirstOrDefault(collection => collection.Title == Title);
    }

    public bool Create(Collection newCollection)
    {
        _context.Add(newCollection);
        return Save();
    }

    public bool Delete(Collection collection)
    {
        _context.Collections.Remove(collection);
        return Save();
    }

    public bool Update(Collection updatedCollection)
    {
        _context.Collections.Update(updatedCollection);
        return Save();
    }

    public bool Save()
    {
        try
        {
            _context.SaveChanges();
        }
        catch
        {
            return false;
        }
        return true;
    }
}