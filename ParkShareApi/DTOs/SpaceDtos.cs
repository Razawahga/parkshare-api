using ParkShareApi.Models;

namespace ParkShareApi.DTOs;

public class CreateSpaceRequest
{
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public SpaceType Type { get; set; } = SpaceType.OpenLot;
    public decimal PricePerDay { get; set; }
    public decimal? PricePerWeek { get; set; }
    public decimal? PricePerMonth { get; set; }
    public decimal? PricePerQuarter { get; set; }
    public decimal? LengthM { get; set; }
    public decimal? WidthM { get; set; }
    public decimal? HeightM { get; set; }
    // List of amenities e.g. ["CCTV", "Covered", "Security Guard"]
    public List<string> Amenities { get; set; } = [];
    public Guid OwnerId { get; set; }
}

public class SpaceResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public bool IsSponsored { get; set; }
    public bool IsBoosted { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Rating { get; set; }
    public int ReviewCount { get; set; }
    public decimal PricePerDay { get; set; }
    public decimal? PricePerWeek { get; set; }
    public decimal? PricePerMonth { get; set; }
    public decimal? PricePerQuarter { get; set; }
    public decimal? LengthM { get; set; }
    public decimal? WidthM { get; set; }
    public decimal? HeightM { get; set; }
    public List<string> Amenities { get; set; } = [];
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public int BookingsCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
