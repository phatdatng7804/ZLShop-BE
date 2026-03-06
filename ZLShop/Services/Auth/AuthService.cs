using ZLShop.Data;
using ZLShop.Services.Interfaces;
using ZLShop.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using ZLShop.Models.Entities;
using ZLShop.Exceptions;
namespace ZLShop.Services.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;
    public AuthService(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
    {
       var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email || u.Username == request.Username);
        if(existingUser != null)
        {
            throw new BadRequestException("Email hoặc Username đã tồn tại");
        }
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var newUser = new User
        {
            Username = request.Username,
            Password = passwordHash,
            Email = request.Email,
            Phone = request.Phone,
            Gender = request.Gender,
            RoleId = request.RoleId,
            CreatedAt = DateTime.UtcNow,
        };
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return new RegisterResponseDto
        {
            Id = newUser.Id,
            Username = newUser.Username,
            CreatedAt = newUser.CreatedAt
        };
    }
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == request.Username || u.Username == request.Username);
        if(user == null)
        {
            throw new BadRequestException("Email hoặc Username không chính xác");
        }
        if(!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            throw new BadRequestException("Mật khẩu không chính xác");
        }
        
        var token = await _jwtService.GenerateTokenAsync(user);
        
        return new LoginResponseDto
        {
            Token = token
        };
    }
}