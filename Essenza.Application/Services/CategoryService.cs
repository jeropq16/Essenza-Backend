using Essenza.Application.DTOs;
using Essenza.Application.Exceptions;
using Essenza.Application.Interfaces;
using Essenza.Domain.Entities;
using Essenza.Domain.Interfaces;

namespace Essenza.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CategoryDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("No se encontró Category.");

        return ToDto(entity);
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var result = new List<CategoryDto>();

        foreach (var entity in entities)
            result.Add(ToDto(entity));

        return result;
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = new Category(dto.Name);

        await _repository.AddAsync(entity);
        return ToDto(entity);
    }

    private static CategoryDto ToDto(Category entity)
    {
        return new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}
