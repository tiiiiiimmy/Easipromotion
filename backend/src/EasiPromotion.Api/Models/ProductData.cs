namespace EasiPromotion.Api.Models;

public class ProductData
{
    public int Id { get; set; }
    public required string ProductName { get; set; }
    public decimal PriceNZD { get; set; }
    public string Unit { get; set; } = "each";
    public string? ImagePath { get; set; }
    public int PosterId { get; set; }
    public Poster? Poster { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public string FormattedPrice => $"${PriceNZD:F2}/{Unit}";
} 