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

    public List<Collection> GetAll()
    {
        return _context.Collections.ToList();
    }

    public Collection? GetOneById(int Id)
    {
        return _context.Collections.FirstOrDefault(c => c.Id == Id);
    }

    public Collection? GetOneByTitle(string Title)
    {
        return _context.Collections.FirstOrDefault(c => c.Title == Title);
    }

    public void Create(Collection collection)
    {
        throw new NotImplementedException();
    }

    public void Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public void Update(int Id, Collection collection)
    {
        throw new NotImplementedException();
    }
}