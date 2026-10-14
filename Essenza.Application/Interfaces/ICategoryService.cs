using Essenza.Application.DTOs;

namespace Essenza.Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryDto> GetByIdAsync(Guid id);
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
}
