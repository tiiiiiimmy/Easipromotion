using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using EasiPromotion.Api.Data;
using EasiPromotion.Api.DTOs.Products;
using EasiPromotion.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EasiPromotion.Api.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductService> _logger;
    private readonly IWebHostEnvironment _environment;

    public ProductService(
        ApplicationDbContext context,
        ILogger<ProductService> logger,
        IWebHostEnvironment environment)
    {
        _context = context;
        _logger = logger;
        _environment = environment;
    }

    public async Task<Poster> ImportProductsFromCsvAsync(ImportProductsRequest request, int userId)
    {
        // Create poster
        var poster = new Poster
        {
            Name = request.PosterName,
            Description = request.PosterDescription,
            TemplateType = request.TemplateType,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Posters.Add(poster);
        await _context.SaveChangesAsync();

        try
        {
            // Read and validate CSV
            using var reader = new StreamReader(request.CsvFile.OpenReadStream());
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null
            };

            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<CsvProductRow>();

            var products = new List<ProductData>();
            foreach (var record in records)
            {
                // Validate price
                if (record.PriceNZD <= 0)
                {
                    throw new InvalidOperationException($"Invalid price for product {record.ProductName}: {record.PriceNZD}");
                }

                // Handle image path
                string? imagePath = null;
                if (!string.IsNullOrEmpty(record.ImagePath))
                {
                    // TODO: Implement image processing
                    imagePath = record.ImagePath;
                }

                var product = new ProductData
                {
                    ProductName = record.ProductName,
                    PriceNZD = record.PriceNZD,
                    Unit = record.Unit,
                    ImagePath = imagePath,
                    PosterId = poster.Id,
                    CreatedAt = DateTime.UtcNow
                };

                products.Add(product);
            }

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            return poster;
        }
        catch (Exception ex)
        {
            // If anything goes wrong, delete the poster and throw
            _context.Posters.Remove(poster);
            await _context.SaveChangesAsync();

            _logger.LogError(ex, "Error importing products from CSV for poster {PosterId}", poster.Id);
            throw;
        }
    }

    public async Task<IEnumerable<ProductData>> GetProductsByPosterIdAsync(int posterId)
    {
        return await _context.Products
            .Where(p => p.PosterId == posterId)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> DeletePosterAsync(int posterId, int userId)
    {
        var poster = await _context.Posters
            .FirstOrDefaultAsync(p => p.Id == posterId && p.UserId == userId);

        if (poster == null)
        {
            return false;
        }

        // Delete associated files
        if (!string.IsNullOrEmpty(poster.WatermarkedImagePath))
        {
            var watermarkedPath = Path.Combine(_environment.WebRootPath, poster.WatermarkedImagePath);
            if (File.Exists(watermarkedPath))
            {
                File.Delete(watermarkedPath);
            }
        }

        if (!string.IsNullOrEmpty(poster.HighResImagePath))
        {
            var highResPath = Path.Combine(_environment.WebRootPath, poster.HighResImagePath);
            if (File.Exists(highResPath))
            {
                File.Delete(highResPath);
            }
        }

        if (!string.IsNullOrEmpty(poster.PdfPath))
        {
            var pdfPath = Path.Combine(_environment.WebRootPath, poster.PdfPath);
            if (File.Exists(pdfPath))
            {
                File.Delete(pdfPath);
            }
        }

        _context.Posters.Remove(poster);
        await _context.SaveChangesAsync();

        return true;
    }
} 