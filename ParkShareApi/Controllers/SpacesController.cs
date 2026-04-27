using Microsoft.AspNetCore.Mvc;
using ParkShareApi.Common;
using ParkShareApi.DTOs;
using ParkShareApi.Models;
using ParkShareApi.Services;

namespace ParkShareApi.Controllers;

[ApiController]
[Route("api/spaces")]
public class SpacesController(SpaceService spaceService) : ControllerBase
{
    // GET api/spaces?city=Lahore&type=Garage
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? city, [FromQuery] SpaceType? type)
    {
        var spaces = await spaceService.GetAllAsync(city, type);
        return Ok(ApiResponse<List<SpaceResponse>>.Success(spaces));
    }

    // GET api/spaces/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var space = await spaceService.GetByIdAsync(id);
        if (space == null)
            return NotFound(ApiResponse<object>.Fail(404, "Parking space not found"));

        return Ok(ApiResponse<SpaceResponse>.Success(space));
    }

    // GET api/spaces/owner/{ownerId}
    [HttpGet("owner/{ownerId:guid}")]
    public async Task<IActionResult> GetByOwner(Guid ownerId)
    {
        var spaces = await spaceService.GetByOwnerAsync(ownerId);
        return Ok(ApiResponse<List<SpaceResponse>>.Success(spaces));
    }

    // POST api/spaces
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpaceRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Address))
            return BadRequest(ApiResponse<object>.Fail(400, "Title and address are required"));

        if (request.PricePerDay <= 0)
            return BadRequest(ApiResponse<object>.Fail(400, "Price per day must be greater than zero"));

        var space = await spaceService.CreateAsync(request);
        return StatusCode(201, ApiResponse<SpaceResponse>.Created(space));
    }

    // PATCH api/spaces/{id}/availability
    [HttpPatch("{id:guid}/availability")]
    public async Task<IActionResult> ToggleAvailability(Guid id)
    {
        var success = await spaceService.ToggleAvailabilityAsync(id);
        if (!success)
            return NotFound(ApiResponse<object>.Fail(404, "Parking space not found"));

        return Ok(ApiResponse<object>.Success(null!, "Availability updated"));
    }
}
