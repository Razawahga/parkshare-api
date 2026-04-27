namespace ParkShareApi.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? AvatarUrl { get; set; }
    public string? City { get; set; }
    public bool IsVerified { get; set; } = false;
    public bool IsPlus { get; set; } = false;
    public DateTime MemberSince { get; set; } = DateTime.UtcNow;
    public decimal Rating { get; set; } = 0;
    public int ReviewCount { get; set; } = 0;

    // Stored as comma-separated string e.g. "seeker" or "seeker,owner"
    public string Roles { get; set; } = "seeker";

    // Navigation properties
    public List<ParkingSpace> OwnedSpaces { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
}
