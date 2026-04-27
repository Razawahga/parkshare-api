using Microsoft.AspNetCore.Mvc;
using ParkShareApi.Common;
using ParkShareApi.DTOs;
using ParkShareApi.Services;

namespace ParkShareApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(UserService userService) : ControllerBase
{
    // POST api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(ApiResponse<object>.Fail(400, "Name, email and password are required"));
        }

        if (await userService.EmailExistsAsync(request.Email))
            return Conflict(ApiResponse<object>.Fail(409, "Email already registered"));

        var user = await userService.RegisterAsync(request);
        return StatusCode(201, ApiResponse<UserResponse>.Created(UserService.ToResponse(user)));
    }

    // POST api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await userService.LoginAsync(request.Email, request.Password);

        if (user == null)
            return Unauthorized(ApiResponse<object>.Fail(401, "Invalid email or password"));

        return Ok(ApiResponse<UserResponse>.Success(UserService.ToResponse(user), "Login successful"));
    }
}
