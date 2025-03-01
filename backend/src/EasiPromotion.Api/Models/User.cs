using System.Text.Json.Serialization;

namespace EasiPromotion.Api.Models;

public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public int RoleId { get; set; }
    
    [JsonIgnore]
    public Role? Role { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
} 