using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParkShareApi.Data;
using ParkShareApi.DTOs;
using ParkShareApi.Models;

namespace ParkShareApi.Services;

public class UserService(AppDbContext db)
{
    private readonly PasswordHasher<User> _hasher = new();

    public async Task<User?> GetByIdAsync(Guid id)
        => await db.Users.FindAsync(id);

    public async Task<User?> GetByEmailAsync(string email)
        => await db.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());

    public async Task<bool> EmailExistsAsync(string email)
        => await db.Users.AnyAsync(u => u.Email == email.ToLower());

    public async Task<User> RegisterAsync(RegisterRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email.ToLower(),
            Phone = request.Phone,
            City = request.City,
            Roles = request.Roles
        };
        user.PasswordHash = _hasher.HashPassword(user, request.Password);

        db.Users.Add(user);
        await db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await GetByEmailAsync(email);
        if (user == null) return null;

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success ? user : null;
    }

    public static UserResponse ToResponse(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Phone = user.Phone,
        AvatarUrl = user.AvatarUrl,
        City = user.City,
        IsVerified = user.IsVerified,
        IsPlus = user.IsPlus,
        MemberSince = user.MemberSince,
        Rating = user.Rating,
        ReviewCount = user.ReviewCount,
        Roles = user.Roles.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
    };
}
