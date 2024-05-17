using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollectionApi.Dto;
using MovieCollectionApi.Services;

namespace MovieCollectionApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class CollectionsController : ControllerBase
{
    public readonly CollectionService _service;

    public CollectionsController(CollectionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? title)
    {
        var collection = await _service.GetAllAsync(title);
        return Ok(collection);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var collection = await _service.GetOneAsync(id);
        return collection is null ? NotFound() : Ok(collection);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCollectionDto dto)
    {
        bool? collectionCreated = await _service.CreateAsync(dto);
        if (collectionCreated is null || !(bool)collectionCreated) { return BadRequest(); }
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] UpdateCollectionDto dto, int id)
    {
        bool? collectionUpdated = await _service.UpdateAsync(dto, id);
        if (collectionUpdated is null)
            return NotFound();
        return (bool)collectionUpdated ? Ok() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool? collectionDeleted = await _service.DeleteAsync(id);
        if (collectionDeleted is null)
            return NotFound();
        return (bool)collectionDeleted ? NoContent() : BadRequest();
    }

    [HttpPost("{collectionId}/movies")]
    public async Task<IActionResult> AddMovie(int collectionId, [FromQuery] int movieId)
    {
        var done = await _service.AddMovieToCollection(collectionId, movieId);
        if (done is null)
            return NotFound("Collection or Movie with specified Id is not found.");
        else if (done is false)
            return BadRequest();
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpDelete("{collectionId}/movies/{movieId}")]
    public async Task<IActionResult> RemoveMovie(int collectionId, int movieId)
    {
        var done = await _service.DeleteMovieFromCollection(collectionId, movieId);
        if (done is null) { return BadRequest(); }
        return NoContent();
    }
}