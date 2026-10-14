using Essenza.Application.DTOs;
using Essenza.Application.Exceptions;
using Essenza.Application.Interfaces;
using Essenza.Domain.Entities;
using Essenza.Domain.Interfaces;

namespace Essenza.Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository;

    public BrandService(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<BrandDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("No se encontró Brand.");

        return ToDto(entity);
    }

    public async Task<List<BrandDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var result = new List<BrandDto>();

        foreach (var entity in entities)
            result.Add(ToDto(entity));

        return result;
    }

    public async Task<BrandDto> CreateAsync(CreateBrandDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = new Brand(dto.Name);

        await _repository.AddAsync(entity);
        return ToDto(entity);
    }

    private static BrandDto ToDto(Brand entity)
    {
        return new BrandDto
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}
