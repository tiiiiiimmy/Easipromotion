using EasiPromotion.Api.DTOs.Auth;

namespace EasiPromotion.Api.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
} 