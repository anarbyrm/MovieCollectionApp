using MovieCollectionApi.Dto;
using MovieCollectionApi.Models;
using MovieCollectionApi.Repository;

namespace MovieCollectionApi.Services;

public class CollectionService
{
    public readonly CollectionRepository _repository;

    public CollectionService(CollectionRepository repository)
    {
        _repository = repository;
    }

    public List<Collection> GetAll()
    {
        return _repository.GetAll();
    }

    public Collection? GetOne(int id)
    {
        return _repository.GetOneById(id);
    }

    public Collection? GetOne(string title)
    {
        return _repository.GetOneByTitle(title);
    }
    
    public bool Create(CreateCollectionDto dto)
    {
        // todo: add automapper 
        Collection newCollection = new() {
            Title = dto.Title
        };
        return _repository.Create(newCollection);
    }

    public bool? Update(UpdateCollectionDto dto, int id)
    {
        Collection? collection = GetOne(id);
        if (collection is null)
            return null;

        // todo: add automapper 
        collection.Title = dto.Title;
        return _repository.Update(collection);
    }

    public bool? Delete(int id)
    {
        Collection? collection = GetOne(id);
        if (collection is null)
            return null;
        return _repository.Delete(collection);
    }
}