using Essenza.Application.DTOs;

namespace Essenza.Application.Interfaces;

public interface IProductVariantService
{
    Task<ProductVariantDto> GetByIdAsync(Guid id);
    Task<List<ProductVariantDto>> GetByProductIdAsync(Guid productId);
    Task<ProductVariantDto> CreateAsync(CreateProductVariantDto dto);
    Task<ProductVariantDto> UpdateStockAsync(Guid id, UpdateProductVariantStockDto dto);
}
