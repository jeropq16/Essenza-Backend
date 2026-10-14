using Essenza.Application.DTOs;

namespace Essenza.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetByIdAsync(Guid id);
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto> CreateAsync(CreateProductDto dto);
    Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto dto);
}
