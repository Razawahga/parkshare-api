using Microsoft.EntityFrameworkCore;
using ParkShareApi.Data;
using ParkShareApi.DTOs;
using ParkShareApi.Models;

namespace ParkShareApi.Services;

public class BookingService(AppDbContext db)
{
    public async Task<List<BookingResponse>> GetByUserAsync(Guid userId)
    {
        var bookings = await db.Bookings
            .Include(b => b.Space)
            .Include(b => b.Seeker)
            .Where(b => b.SeekerId == userId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return bookings.Select(ToResponse).ToList();
    }

    public async Task<List<BookingResponse>> GetBySpaceAsync(Guid spaceId)
    {
        var bookings = await db.Bookings
            .Include(b => b.Space)
            .Include(b => b.Seeker)
            .Where(b => b.SpaceId == spaceId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return bookings.Select(ToResponse).ToList();
    }

    public async Task<BookingResponse?> GetByIdAsync(Guid id)
    {
        var booking = await db.Bookings
            .Include(b => b.Space)
            .Include(b => b.Seeker)
            .FirstOrDefaultAsync(b => b.Id == id);

        return booking == null ? null : ToResponse(booking);
    }

    public async Task<BookingResponse> CreateAsync(CreateBookingRequest request)
    {
        var booking = new Booking
        {
            SpaceId = request.SpaceId,
            SeekerId = request.SeekerId,
            VehicleName = request.VehicleName,
            VehiclePlate = request.VehiclePlate,
            Period = request.Period,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalAmount = request.TotalAmount,
            Notes = request.Notes,
            Status = BookingStatus.Pending
        };

        db.Bookings.Add(booking);

        // Increase the booking count on the space
        var space = await db.ParkingSpaces.FindAsync(request.SpaceId);
        if (space != null) space.BookingsCount++;

        await db.SaveChangesAsync();
        return await GetByIdAsync(booking.Id) ?? ToResponse(booking);
    }

    public async Task<BookingResponse?> UpdateStatusAsync(Guid id, BookingStatus status)
    {
        var booking = await db.Bookings.FindAsync(id);
        if (booking == null) return null;

        booking.Status = status;
        await db.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public static BookingResponse ToResponse(Booking b) => new()
    {
        Id = b.Id,
        SpaceId = b.SpaceId,
        SpaceName = b.Space?.Title ?? string.Empty,
        SpaceAddress = b.Space?.Address ?? string.Empty,
        SeekerId = b.SeekerId,
        SeekerName = b.Seeker?.Name ?? string.Empty,
        VehicleName = b.VehicleName,
        VehiclePlate = b.VehiclePlate,
        Period = b.Period.ToString(),
        StartDate = b.StartDate,
        EndDate = b.EndDate,
        Status = b.Status.ToString(),
        TotalAmount = b.TotalAmount,
        Notes = b.Notes,
        CreatedAt = b.CreatedAt
    };
}
