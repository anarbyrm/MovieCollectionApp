using System.Text.Json;
using MovieCollectionApi.Dto;
using MovieCollectionApi.Models;
using MovieCollectionApi.Repository;

namespace MovieCollectionApi.Services;

public class CollectionService
{
    public readonly CollectionRepository _repository;
    public readonly MovieProviderService _movieService;

    public CollectionService(CollectionRepository repository, MovieProviderService movieService)
    {
        _repository = repository;
        _movieService = movieService;
    }

    public async Task<List<ListCollectionDto>> GetAllAsync(string? query)
    {
        List<Collection> collections = await _repository.GetAllAsync(query);
        return collections
            .Select(collection => new ListCollectionDto {
                Id = collection.Id,
                Title = collection.Title
            }).ToList();
    }

    public async Task<Collection?> GetOneAsync(int id)
    {
        return await _repository.GetCollectionByIdAsync(id, includeRelations: true);
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

    public async Task<bool?> AddMovieToCollection(int collectionId, int movieId)
    {
        Collection? collection = await _repository.GetCollectionByIdAsync(collectionId, includeRelations: true);
        var rawMovieData = await _movieService.FetchMovieDetailById(movieId);
        if (collection is null || rawMovieData is null)
            return null;

        var movieData = JsonSerializer.Deserialize<MovieConvertDto>(rawMovieData);

        Movie movie = new()
        {
            Id = movieData.id,
            ImdbId = movieData.imdb_id,
            Title = movieData.title,
            Overview = movieData.overview,
            PosterPath = movieData.poster_path
        };
        return await _repository.AddMovieToCollection(collection, movie);
    }

    public async Task<bool?> DeleteMovieFromCollection(int collectionId, int movieId)
    {
        Collection? collection = await _repository.GetCollectionByIdAsync(
            collectionId, includeRelations: true);
        if (collection is null) { return null; }
        Movie? movie = _repository.GetCollectionMovieById(collection, movieId);
        if (movie is null) { return null; }
        return await _repository.DeleteMovieFromCollection(collection, movie);
    }
}
