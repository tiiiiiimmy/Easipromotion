using CsvHelper.Configuration.Attributes;

namespace EasiPromotion.Api.DTOs.Products;

public class CsvProductRow
{
    [Name("ProductName")]
    public required string ProductName { get; set; }

    [Name("PriceNZD")]
    public required decimal PriceNZD { get; set; }

    [Name("Unit")]
    public string Unit { get; set; } = "each";

    [Name("ImagePath")]
    public string? ImagePath { get; set; }
} 