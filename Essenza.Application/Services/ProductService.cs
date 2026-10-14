using Essenza.Application.DTOs;
using Essenza.Application.Exceptions;
using Essenza.Application.Interfaces;
using Essenza.Domain.Entities;
using Essenza.Domain.Interfaces;

namespace Essenza.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository repository, IBrandRepository brandRepository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("No se encontró Product.");

        return ToDto(entity);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var result = new List<ProductDto>();

        foreach (var entity in entities)
            result.Add(ToDto(entity));

        return result;
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (await _brandRepository.GetByIdAsync(dto.BrandId) is null)
            throw new NotFoundException("La marca no existe.");

        if (await _categoryRepository.GetByIdAsync(dto.CategoryId) is null)
            throw new NotFoundException("La categoría no existe.");

        var entity = new Product(dto.Name, dto.Description, dto.BrandId,
            dto.CategoryId, dto.Gender, dto.Price);
        entity.UpdatePrices(dto.Price, dto.DiscountPrice);
        entity.IsNew = dto.IsNew;
        entity.IsActive = dto.IsActive;

        await _repository.AddAsync(entity);
        return ToDto(entity);
    }

    public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("No se encontró Product.");

        entity.UpdatePrices(dto.Price, dto.DiscountPrice);
        entity.IsNew = dto.IsNew;
        entity.IsActive = dto.IsActive;

        await _repository.UpdateAsync(entity);
        return ToDto(entity);
    }

    private static ProductDto ToDto(Product entity)
    {
        return new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            BrandId = entity.BrandId,
            CategoryId = entity.CategoryId,
            Gender = entity.Gender,
            Price = entity.Price,
            DiscountPrice = entity.DiscountPrice,
            IsNew = entity.IsNew,
            IsActive = entity.IsActive,
            CurrentPrice = entity.GetCurrentPrice(),
        };
    }
}
