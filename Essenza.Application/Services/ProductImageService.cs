using Essenza.Application.DTOs;
using Essenza.Application.Exceptions;
using Essenza.Application.Interfaces;
using Essenza.Domain.Entities;
using Essenza.Domain.Interfaces;

namespace Essenza.Application.Services;

public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _repository;
    private readonly IProductRepository _productRepository;

    public ProductImageService(IProductImageRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<ProductImageDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("No se encontró ProductImage.");

        return ToDto(entity);
    }

    public async Task<List<ProductImageDto>> GetByProductIdAsync(Guid productId)
    {
        if (await _productRepository.GetByIdAsync(productId) is null)
            throw new NotFoundException("El producto no existe.");

        var entities = await _repository.GetByProductIdAsync(productId);
        var result = new List<ProductImageDto>();

        foreach (var entity in entities)
            result.Add(ToDto(entity));

        return result;
    }

    public async Task<ProductImageDto> CreateAsync(CreateProductImageDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (await _productRepository.GetByIdAsync(dto.ProductId) is null)
            throw new NotFoundException("El producto no existe.");

        var entity = new ProductImage(dto.ProductId, dto.Url, dto.DisplayOrder);

        await _repository.AddAsync(entity);
        return ToDto(entity);
    }

    private static ProductImageDto ToDto(ProductImage entity)
    {
        return new ProductImageDto
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            Url = entity.Url,
            DisplayOrder = entity.DisplayOrder,
        };
    }
}
