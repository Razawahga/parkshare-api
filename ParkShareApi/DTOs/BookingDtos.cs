using ParkShareApi.Models;

namespace ParkShareApi.DTOs;

public class CreateBookingRequest
{
    public Guid SpaceId { get; set; }
    public Guid SeekerId { get; set; }
    public string VehicleName { get; set; } = string.Empty;
    public string VehiclePlate { get; set; } = string.Empty;
    public BookingPeriod Period { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
}

public class UpdateBookingStatusRequest
{
    public BookingStatus Status { get; set; }
}

public class BookingResponse
{
    public Guid Id { get; set; }
    public Guid SpaceId { get; set; }
    public string SpaceName { get; set; } = string.Empty;
    public string SpaceAddress { get; set; } = string.Empty;
    public Guid SeekerId { get; set; }
    public string SeekerName { get; set; } = string.Empty;
    public string VehicleName { get; set; } = string.Empty;
    public string VehiclePlate { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
