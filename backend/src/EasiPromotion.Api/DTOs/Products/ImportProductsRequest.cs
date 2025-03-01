using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EasiPromotion.Api.DTOs.Products;

public class ImportProductsRequest
{
    [Required]
    public required IFormFile CsvFile { get; set; }

    [Required]
    public required string PosterName { get; set; }

    public string? PosterDescription { get; set; }

    [Required]
    public required string TemplateType { get; set; } = "grid";
} 