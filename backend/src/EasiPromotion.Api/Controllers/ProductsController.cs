using System.Security.Claims;
using EasiPromotion.Api.DTOs.Products;
using EasiPromotion.Api.Models;
using EasiPromotion.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasiPromotion.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpPost("import")]
    [Authorize(Roles = "Member,Admin")]
    [RequestSizeLimit(10 * 1024 * 1024)] // 10MB limit
    public async Task<ActionResult<Poster>> ImportProducts([FromForm] ImportProductsRequest request)
    {
        _logger.LogInformation(
        "Received CSV: {CsvFile}, PosterName: {PosterName}, TemplateType: {TemplateType}",
        request.CsvFile?.FileName, 
        request.PosterName, 
        request.TemplateType
         );
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var poster = await _productService.ImportProductsFromCsvAsync(request, userId);
            return Ok(poster);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid data in CSV import");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing products from CSV");
            return StatusCode(500, new { message = "An error occurred while importing products" });
        }
    }

    [HttpGet("poster/{posterId}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ProductData>>> GetProductsByPosterId(int posterId)
    {
        try
        {
            var products = await _productService.GetProductsByPosterIdAsync(posterId);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products for poster {PosterId}", posterId);
            return StatusCode(500, new { message = "An error occurred while retrieving products" });
        }
    }

    [HttpDelete("poster/{posterId}")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> DeletePoster(int posterId)
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _productService.DeletePosterAsync(posterId, userId);

            if (!result)
            {
                return NotFound(new { message = "Poster not found or you don't have permission to delete it" });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting poster {PosterId}", posterId);
            return StatusCode(500, new { message = "An error occurred while deleting the poster" });
        }
    }
} 