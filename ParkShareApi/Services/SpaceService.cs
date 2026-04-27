using Microsoft.EntityFrameworkCore;
using ParkShareApi.Data;
using ParkShareApi.DTOs;
using ParkShareApi.Models;

namespace ParkShareApi.Services;

public class SpaceService(AppDbContext db)
{
    public async Task<List<SpaceResponse>> GetAllAsync(string? city = null, SpaceType? type = null)
    {
        var query = db.ParkingSpaces
            .Include(s => s.Owner)
            .Where(s => s.IsAvailable);

        if (!string.IsNullOrEmpty(city))
            query = query.Where(s => s.City.ToLower() == city.ToLower());

        if (type.HasValue)
            query = query.Where(s => s.Type == type);

        var spaces = await query.ToListAsync();
        return spaces.Select(ToResponse).ToList();
    }

    public async Task<SpaceResponse?> GetByIdAsync(Guid id)
    {
        var space = await db.ParkingSpaces
            .Include(s => s.Owner)
            .FirstOrDefaultAsync(s => s.Id == id);

        return space == null ? null : ToResponse(space);
    }

    public async Task<List<SpaceResponse>> GetByOwnerAsync(Guid ownerId)
    {
        var spaces = await db.ParkingSpaces
            .Include(s => s.Owner)
            .Where(s => s.OwnerId == ownerId)
            .ToListAsync();

        return spaces.Select(ToResponse).ToList();
    }

    public async Task<SpaceResponse> CreateAsync(CreateSpaceRequest request)
    {
        var space = new ParkingSpace
        {
            Title = request.Title,
            Address = request.Address,
            City = request.City,
            Type = request.Type,
            PricePerDay = request.PricePerDay,
            PricePerWeek = request.PricePerWeek,
            PricePerMonth = request.PricePerMonth,
            PricePerQuarter = request.PricePerQuarter,
            LengthM = request.LengthM,
            WidthM = request.WidthM,
            HeightM = request.HeightM,
            Amenities = string.Join(',', request.Amenities),
            OwnerId = request.OwnerId
        };

        db.ParkingSpaces.Add(space);
        await db.SaveChangesAsync();

        return await GetByIdAsync(space.Id) ?? ToResponse(space);
    }

    public async Task<bool> ToggleAvailabilityAsync(Guid id)
    {
        var space = await db.ParkingSpaces.FindAsync(id);
        if (space == null) return false;

        space.IsAvailable = !space.IsAvailable;
        await db.SaveChangesAsync();
        return true;
    }

    public static SpaceResponse ToResponse(ParkingSpace s) => new()
    {
        Id = s.Id,
        Title = s.Title,
        Address = s.Address,
        City = s.City,
        Type = s.Type.ToString(),
        IsVerified = s.IsVerified,
        IsSponsored = s.IsSponsored,
        IsBoosted = s.IsBoosted,
        IsAvailable = s.IsAvailable,
        Rating = s.Rating,
        ReviewCount = s.ReviewCount,
        PricePerDay = s.PricePerDay,
        PricePerWeek = s.PricePerWeek,
        PricePerMonth = s.PricePerMonth,
        PricePerQuarter = s.PricePerQuarter,
        LengthM = s.LengthM,
        WidthM = s.WidthM,
        HeightM = s.HeightM,
        Amenities = string.IsNullOrEmpty(s.Amenities)
            ? []
            : s.Amenities.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
        OwnerId = s.OwnerId,
        OwnerName = s.Owner?.Name ?? string.Empty,
        BookingsCount = s.BookingsCount,
        CreatedAt = s.CreatedAt
    };
}
