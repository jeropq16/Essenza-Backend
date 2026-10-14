using Essenza.Application.DTOs;
using Essenza.Application.Exceptions;
using Essenza.Application.Interfaces;
using Essenza.Domain.Entities;
using Essenza.Domain.Interfaces;

namespace Essenza.Application.Services;

public class ProductVariantService : IProductVariantService
{
    private readonly IProductVariantRepository _repository;
    private readonly IProductRepository _productRepository;

    public ProductVariantService(IProductVariantRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<ProductVariantDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("No se encontró ProductVariant.");

        return ToDto(entity);
    }

    public async Task<List<ProductVariantDto>> GetByProductIdAsync(Guid productId)
    {
        if (await _productRepository.GetByIdAsync(productId) is null)
            throw new NotFoundException("El producto no existe.");

        var entities = await _repository.GetByProductIdAsync(productId);
        var result = new List<ProductVariantDto>();

        foreach (var entity in entities)
            result.Add(ToDto(entity));

        return result;
    }

    public async Task<ProductVariantDto> CreateAsync(CreateProductVariantDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (await _productRepository.GetByIdAsync(dto.ProductId) is null)
            throw new NotFoundException("El producto no existe.");

        var entity = new ProductVariant(dto.ProductId, dto.Size, dto.Color, dto.Stock);

        await _repository.AddAsync(entity);
        return ToDto(entity);
    }

    public async Task<ProductVariantDto> UpdateStockAsync(Guid id, UpdateProductVariantStockDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("No se encontró ProductVariant.");

        entity.UpdateStock(dto.Stock);

        await _repository.UpdateAsync(entity);
        return ToDto(entity);
    }

    private static ProductVariantDto ToDto(ProductVariant entity)
    {
        return new ProductVariantDto
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            Size = entity.Size,
            Color = entity.Color,
            Stock = entity.Stock,
        };
    }
}
