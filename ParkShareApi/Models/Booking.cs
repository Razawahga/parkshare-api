namespace ParkShareApi.Models;

public enum BookingPeriod { Day, Week, Month, Quarter }
public enum BookingStatus { Pending, Confirmed, Active, Declined, Completed }

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SpaceId { get; set; }
    public ParkingSpace? Space { get; set; }

    public Guid SeekerId { get; set; }
    public User? Seeker { get; set; }

    public string VehicleName { get; set; } = string.Empty;
    public string VehiclePlate { get; set; } = string.Empty;
    public BookingPeriod Period { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
