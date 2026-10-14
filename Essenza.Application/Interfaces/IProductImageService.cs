using Essenza.Application.DTOs;

namespace Essenza.Application.Interfaces;

public interface IProductImageService
{
    Task<ProductImageDto> GetByIdAsync(Guid id);
    Task<List<ProductImageDto>> GetByProductIdAsync(Guid productId);
    Task<ProductImageDto> CreateAsync(CreateProductImageDto dto);
}
