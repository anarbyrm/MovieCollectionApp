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

    public async Task<List<Collection>> GetAllAsync(string? query)
    {
        return await _repository.GetAllAsync(query);
    }

    public async Task<Collection?> GetOneAsync(int id)
    {
        return await _repository.GetOneByIdAsync(id);
    }
    
    public async Task<bool> CreateAsync(CreateCollectionDto dto)
    {
        // todo: add automapper 
        Collection newCollection = new() {
            Title = dto.Title
        };
        return await _repository.CreateAsync(newCollection);
    }

    public async Task<bool?> UpdateAsync(UpdateCollectionDto dto, int id)
    {
        Collection? collection = await GetOneAsync(id);
        if (collection is null)
            return null;

        // todo: add automapper 
        collection.Title = dto.Title;
        return await _repository.UpdateAsync(collection);
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        Collection? collection = await GetOneAsync(id);
        if (collection is null)
            return null;
        return await _repository.DeleteAsync(collection);
    }
}