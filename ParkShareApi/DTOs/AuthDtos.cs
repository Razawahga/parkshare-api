namespace ParkShareApi.DTOs;

public class RegisterRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? City { get; set; }
    // "seeker", "owner", or "seeker,owner"
    public string Roles { get; set; } = "seeker";
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? AvatarUrl { get; set; }
    public string? City { get; set; }
    public bool IsVerified { get; set; }
    public bool IsPlus { get; set; }
    public DateTime MemberSince { get; set; }
    public decimal Rating { get; set; }
    public int ReviewCount { get; set; }
    public List<string> Roles { get; set; } = [];
}
