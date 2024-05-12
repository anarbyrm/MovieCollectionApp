using Microsoft.AspNetCore.Mvc;
using MovieCollectionApi.Dto;
using MovieCollectionApi.Services;

namespace MovieCollectionApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CollectionsController : ControllerBase
{
    public readonly CollectionService _service;

    public CollectionsController(CollectionService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllCollections([FromQuery] string? title)
    {
        if (title is null)
            return Ok(_service.GetAll());

        var collection = _service.GetOne(title);
        return collection is null ? NotFound() : Ok(collection);
    }

    [HttpGet("{id}")]
    public IActionResult GetOneCollection(int id)
    {
        var collection = _service.GetOne(id);
        return collection is null ? NotFound() : Ok();
    }

    [HttpPost]
    public IActionResult CreateCollection([FromBody] CreateCollectionDto dto)
    {
        bool collectionCreated = _service.Create(dto);
        return collectionCreated ? Created() : BadRequest();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCollection([FromBody] UpdateCollectionDto dto, int id)
    {
        bool? collectionUpdated = _service.Update(dto, id);
        if (collectionUpdated is null)
            return NotFound();
        return (bool)collectionUpdated ? Ok() : BadRequest();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCollection(int id)
    {
        bool? collectionDeleted = _service.Delete(id);
        if (collectionDeleted is null)
            return NotFound();
        return (bool)collectionDeleted ? NoContent() : BadRequest();
    }
}