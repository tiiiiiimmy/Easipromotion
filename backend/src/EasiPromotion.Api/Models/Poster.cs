using System.Text.Json.Serialization;

namespace EasiPromotion.Api.Models;

public class Poster
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string TemplateType { get; set; } = "grid";  // grid, hero
    public string? WatermarkedImagePath { get; set; }
    public string? HighResImagePath { get; set; }
    public string? PdfPath { get; set; }
    public int UserId { get; set; }
    
    [JsonIgnore]
    public User? User { get; set; }
    
    public ICollection<ProductData> Products { get; set; } = new List<ProductData>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public static class TemplateTypes
    {
        public const string Grid = "grid";
        public const string Hero = "hero";
    }
} 