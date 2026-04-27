using Microsoft.AspNetCore.Mvc;
using ParkShareApi.Common;
using ParkShareApi.DTOs;
using ParkShareApi.Models;
using ParkShareApi.Services;

namespace ParkShareApi.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController(BookingService bookingService) : ControllerBase
{
    // GET api/bookings/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var booking = await bookingService.GetByIdAsync(id);
        if (booking == null)
            return NotFound(ApiResponse<object>.Fail(404, "Booking not found"));

        return Ok(ApiResponse<BookingResponse>.Success(booking));
    }

    // GET api/bookings/user/{userId}
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var bookings = await bookingService.GetByUserAsync(userId);
        return Ok(ApiResponse<List<BookingResponse>>.Success(bookings));
    }

    // GET api/bookings/space/{spaceId}
    [HttpGet("space/{spaceId:guid}")]
    public async Task<IActionResult> GetBySpace(Guid spaceId)
    {
        var bookings = await bookingService.GetBySpaceAsync(spaceId);
        return Ok(ApiResponse<List<BookingResponse>>.Success(bookings));
    }

    // POST api/bookings
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.VehicleName) || string.IsNullOrWhiteSpace(request.VehiclePlate))
            return BadRequest(ApiResponse<object>.Fail(400, "Vehicle name and plate are required"));

        if (request.StartDate >= request.EndDate)
            return BadRequest(ApiResponse<object>.Fail(400, "End date must be after start date"));

        var booking = await bookingService.CreateAsync(request);
        return StatusCode(201, ApiResponse<BookingResponse>.Created(booking, "Booking submitted successfully"));
    }

    // PATCH api/bookings/{id}/status
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateBookingStatusRequest request)
    {
        var booking = await bookingService.UpdateStatusAsync(id, request.Status);
        if (booking == null)
            return NotFound(ApiResponse<object>.Fail(404, "Booking not found"));

        return Ok(ApiResponse<BookingResponse>.Success(booking, $"Booking {request.Status.ToString().ToLower()}"));
    }
}
