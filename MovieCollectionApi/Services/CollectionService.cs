using System.Text.Json;
using MovieCollectionApi.Dto;
using MovieCollectionApi.Models;
using MovieCollectionApi.Repository;

namespace MovieCollectionApi.Services;

public class CollectionService
{
    public readonly ICollectionRepository _repository;
    public readonly MovieProviderService _movieExternalService;
    public readonly IMovieRepository _movieRepository;

    public CollectionService(
        ICollectionRepository repository, 
        MovieProviderService movieExternalService,
        IMovieRepository movieRepository)
    {
        _repository = repository;
        _movieExternalService = movieExternalService;
        _movieRepository = movieRepository;
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
        var rawMovieData = await _movieExternalService.FetchMovieDetailById(movieId);
        if (collection is null || rawMovieData is null)
            return null;

        var movieData = JsonSerializer.Deserialize<MovieConvertDto>(rawMovieData);
        bool movieExists = await _movieRepository.CheckMovieExistsAsync(movieId);
        Movie? movie = null;

        if (!movieExists) {
            movie = new()
            {
                Id = movieId,
                ImdbId = movieData.imdb_id,
                Title = movieData.title,
                Overview = movieData.overview,
                PosterPath = movieData.poster_path
            };
        }
        else
        {
            movie = await _movieRepository.FetchMovieById(movieId);
        }

        if (movie is null) { return null; }
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
