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
    public IActionResult GetAllCollections([FromQuery] string? Title)
    {
        if (Title is null)
            return Ok(_service.GetAll());

        var collection = _service.GetOne(Title);
        if (collection is null)
            return NotFound();
        return Ok(collection);
    }

    [HttpGet("{id}")]
    public IActionResult GetOneCollection(int Id)
    {
        var collection = _service.GetOne(Id);

        if (collection is null)
            return NotFound();
        return Ok(collection);
    }

    [HttpPost]
    public IActionResult CreateCollection([FromBody] CreateCollectionDto dto)
    {
        bool collectionCreated = _service.Create(dto);
        return collectionCreated ? Created() : BadRequest();
    }
}