namespace ParkShareApi.Models;

public enum SpaceType { Garage, OpenLot, Covered, Plaza }

public class ParkingSpace
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public SpaceType Type { get; set; } = SpaceType.OpenLot;
    public bool IsVerified { get; set; } = false;
    public bool IsSponsored { get; set; } = false;
    public bool IsBoosted { get; set; } = false;
    public bool IsAvailable { get; set; } = true;
    public decimal Rating { get; set; } = 0;
    public int ReviewCount { get; set; } = 0;

    // Pricing
    public decimal PricePerDay { get; set; }
    public decimal? PricePerWeek { get; set; }
    public decimal? PricePerMonth { get; set; }
    public decimal? PricePerQuarter { get; set; }

    // Dimensions (meters)
    public decimal? LengthM { get; set; }
    public decimal? WidthM { get; set; }
    public decimal? HeightM { get; set; }

    // Stored as comma-separated string e.g. "CCTV,Covered,Security Guard"
    public string Amenities { get; set; } = string.Empty;

    // Owner
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }

    public int BookingsCount { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public List<Booking> Bookings { get; set; } = [];
}
