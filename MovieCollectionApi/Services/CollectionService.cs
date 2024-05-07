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

    public Collection? GetOne(int Id)
    {
        return _repository.GetOneById(Id);
    }

    public Collection? GetOne(string Title)
    {
        return _repository.GetOneByTitle(Title);
    }
    
    public bool Create(CreateCollectionDto dto)
    {
        Collection newCollection = new() {
            Title = dto.Title
        };
        return _repository.Create(newCollection);
    }
}