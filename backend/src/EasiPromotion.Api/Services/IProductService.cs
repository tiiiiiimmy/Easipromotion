using EasiPromotion.Api.DTOs.Products;
using EasiPromotion.Api.Models;

namespace EasiPromotion.Api.Services;

public interface IProductService
{
    Task<Poster> ImportProductsFromCsvAsync(ImportProductsRequest request, int userId);
    Task<IEnumerable<ProductData>> GetProductsByPosterIdAsync(int posterId);
    Task<bool> DeletePosterAsync(int posterId, int userId);
} 