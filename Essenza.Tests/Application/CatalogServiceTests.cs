using Essenza.Application.DTOs;
using Essenza.Application.Exceptions;
using Essenza.Application.Services;
using Essenza.Domain.Entities;
using Essenza.Domain.Enums;

namespace Essenza.Tests.Application;

public class CatalogServiceTests
{
    private readonly FakeBrandRepository _brands = new();
    private readonly FakeCategoryRepository _categories = new();
    private readonly FakeProductRepository _products = new();

    [Fact]
    public async Task CreateProduct_SavesValidProductAndReturnsEffectivePrice()
    {
        var request = CreateValidRequest();
        request.DiscountPrice = 80m;

        var result = await ProductService().CreateAsync(request);

        var saved = Assert.Single(_products.Items);
        Assert.Equal(saved.Id, result.Id);
        Assert.Equal(request.BrandId, saved.BrandId);
        Assert.Equal(request.CategoryId, saved.CategoryId);
        Assert.Equal(80m, result.CurrentPrice);
        Assert.True(result.IsActive);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CreateProduct_MissingReferenceDoesNotSave(bool missingBrand)
    {
        var request = CreateValidRequest();
        if (missingBrand)
            request.BrandId = Guid.NewGuid();
        else
            request.CategoryId = Guid.NewGuid();

        await Assert.ThrowsAsync<NotFoundException>(() => ProductService().CreateAsync(request));

        Assert.Empty(_products.Items);
    }

    [Fact]
    public async Task CreateProduct_InvalidDiscountDoesNotSave()
    {
        var request = CreateValidRequest();
        request.DiscountPrice = request.Price;

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => ProductService().CreateAsync(request));

        Assert.Empty(_products.Items);
    }

    [Fact]
    public async Task UpdateProduct_InvalidPricesDoNotChangeFlagsOrPersist()
    {
        var product = await ProductService().CreateAsync(CreateValidRequest());

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            ProductService().UpdateAsync(product.Id, new UpdateProductDto
            {
                Price = 50m, DiscountPrice = 80m, IsActive = false, IsNew = true
            }));

        var saved = Assert.Single(_products.Items);
        Assert.Equal(100m, saved.Price);
        Assert.True(saved.IsActive);
        Assert.False(saved.IsNew);
        Assert.Equal(0, _products.Updates);
    }

    [Fact]
    public async Task UpdateProduct_RemovesDiscountAndPersistsFlags()
    {
        var request = CreateValidRequest();
        request.DiscountPrice = 80m;
        var product = await ProductService().CreateAsync(request);

        var result = await ProductService().UpdateAsync(product.Id,
            new UpdateProductDto { Price = 120m, IsActive = false, IsNew = true });

        Assert.Null(result.DiscountPrice);
        Assert.Equal(120m, result.CurrentPrice);
        Assert.False(result.IsActive);
        Assert.True(result.IsNew);
        Assert.Equal(1, _products.Updates);
    }

    [Fact]
    public async Task MissingProduct_QueriesAndUpdatesReportNotFound()
    {
        var id = Guid.NewGuid();

        await Assert.ThrowsAsync<NotFoundException>(() => ProductService().GetByIdAsync(id));
        await Assert.ThrowsAsync<NotFoundException>(() =>
            ProductService().UpdateAsync(id, new UpdateProductDto { Price = 100m }));

        Assert.Equal(0, _products.Updates);
    }

    [Fact]
    public async Task Variant_CreateRequiresProduct_AndStockUpdateUsesDomainRules()
    {
        var repository = new FakeProductVariantRepository();
        var service = new ProductVariantService(repository, _products);
        var request = new CreateProductVariantDto
        {
            ProductId = Guid.NewGuid(), Size = "M", Color = "Negro", Stock = 5
        };

        await Assert.ThrowsAsync<NotFoundException>(() => service.CreateAsync(request));
        Assert.Empty(repository.Items);

        var product = await ProductService().CreateAsync(CreateValidRequest());
        request.ProductId = product.Id;
        var variant = await service.CreateAsync(request);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            service.UpdateStockAsync(variant.Id, new UpdateProductVariantStockDto { Stock = -1 }));
        Assert.Equal(0, repository.Updates);
        Assert.Equal(5, repository.Items[0].Stock);

        var result = await service.UpdateStockAsync(variant.Id,
            new UpdateProductVariantStockDto { Stock = 0 });
        Assert.Equal(0, result.Stock);
        Assert.Equal(1, repository.Updates);
    }

    [Fact]
    public async Task Images_RequireProduct_AndAreListedOnlyForThatProductInOrder()
    {
        var repository = new FakeProductImageRepository();
        var service = new ProductImageService(repository, _products);
        var request = new CreateProductImageDto { ProductId = Guid.NewGuid(), Url = "/photo.jpg" };

        await Assert.ThrowsAsync<NotFoundException>(() => service.CreateAsync(request));
        Assert.Empty(repository.Items);
        await Assert.ThrowsAsync<NotFoundException>(() => service.GetByProductIdAsync(request.ProductId));

        var product = await ProductService().CreateAsync(CreateValidRequest());
        Assert.Empty(await service.GetByProductIdAsync(product.Id));

        request.ProductId = product.Id;
        request.DisplayOrder = 2;
        await service.CreateAsync(request);
        request.DisplayOrder = 0;
        await service.CreateAsync(request);
        repository.Items.Add(new ProductImage(Guid.NewGuid(), "/other.jpg", 1));

        var result = await service.GetByProductIdAsync(product.Id);
        Assert.Equal(new[] { 0, 2 }, result.Select(image => image.DisplayOrder));
        Assert.All(result, image => Assert.Equal(product.Id, image.ProductId));
    }

    [Fact]
    public async Task BrandAndCategory_RejectBlankNamesWithoutSaving()
    {
        var brands = new BrandService(_brands);
        var categories = new CategoryService(_categories);

        await Assert.ThrowsAsync<ArgumentException>(() => brands.CreateAsync(new CreateBrandDto { Name = " " }));
        await Assert.ThrowsAsync<ArgumentException>(() => categories.CreateAsync(new CreateCategoryDto { Name = " " }));

        Assert.Empty(_brands.Items);
        Assert.Empty(_categories.Items);
    }

    private ProductService ProductService() => new(_products, _brands, _categories);

    private CreateProductDto CreateValidRequest()
    {
        var brand = new Brand("Supreme");
        var category = new Category("Camisetas");
        _brands.Items.Add(brand);
        _categories.Items.Add(category);
        return new CreateProductDto
        {
            Name = "Portrait", BrandId = brand.Id, CategoryId = category.Id,
            Gender = Gender.Unisex, Price = 100m
        };
    }
}
