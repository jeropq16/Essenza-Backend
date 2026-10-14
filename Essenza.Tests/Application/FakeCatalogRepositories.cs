using Essenza.Domain.Entities;
using Essenza.Domain.Interfaces;

namespace Essenza.Tests.Application;

// Repositorios de prueba: permiten comprobar Application sin una base de datos.

internal class FakeBrandRepository : IBrandRepository
{
    public List<Brand> Items { get; } = new();
    public int Updates { get; private set; }

    public Task<Brand?> GetByIdAsync(Guid id)
        => Task.FromResult(Items.Find(item => item.Id == id));

    public Task<List<Brand>> GetAllAsync()
        => Task.FromResult(Items.ToList());

    public Task AddAsync(Brand entity)
    {
        Items.Add(entity);
        return Task.CompletedTask;
    }
}

internal class FakeCategoryRepository : ICategoryRepository
{
    public List<Category> Items { get; } = new();
    public int Updates { get; private set; }

    public Task<Category?> GetByIdAsync(Guid id)
        => Task.FromResult(Items.Find(item => item.Id == id));

    public Task<List<Category>> GetAllAsync()
        => Task.FromResult(Items.ToList());

    public Task AddAsync(Category entity)
    {
        Items.Add(entity);
        return Task.CompletedTask;
    }
}

internal class FakeProductRepository : IProductRepository
{
    public List<Product> Items { get; } = new();
    public int Updates { get; private set; }

    public Task<Product?> GetByIdAsync(Guid id)
        => Task.FromResult(Items.Find(item => item.Id == id));

    public Task<List<Product>> GetAllAsync()
        => Task.FromResult(Items.ToList());

    public Task AddAsync(Product entity)
    {
        Items.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Product entity)
    {
        Updates++;
        return Task.CompletedTask;
    }
}

internal class FakeProductVariantRepository : IProductVariantRepository
{
    public List<ProductVariant> Items { get; } = new();
    public int Updates { get; private set; }

    public Task<ProductVariant?> GetByIdAsync(Guid id)
        => Task.FromResult(Items.Find(item => item.Id == id));

    public Task<List<ProductVariant>> GetByProductIdAsync(Guid productId)
        => Task.FromResult(Items.Where(item => item.ProductId == productId).ToList());

    public Task AddAsync(ProductVariant entity)
    {
        Items.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(ProductVariant entity)
    {
        Updates++;
        return Task.CompletedTask;
    }
}

internal class FakeProductImageRepository : IProductImageRepository
{
    public List<ProductImage> Items { get; } = new();
    public int Updates { get; private set; }

    public Task<ProductImage?> GetByIdAsync(Guid id)
        => Task.FromResult(Items.Find(item => item.Id == id));

    public Task<List<ProductImage>> GetByProductIdAsync(Guid productId)
        => Task.FromResult(Items.Where(item => item.ProductId == productId).OrderBy(item => item.DisplayOrder).ToList());

    public Task AddAsync(ProductImage entity)
    {
        Items.Add(entity);
        return Task.CompletedTask;
    }
}
