using Essenza.Application.DTOs;

namespace Essenza.Application.Interfaces;

public interface IBrandService
{
    Task<BrandDto> GetByIdAsync(Guid id);
    Task<List<BrandDto>> GetAllAsync();
    Task<BrandDto> CreateAsync(CreateBrandDto dto);
}
