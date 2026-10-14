using Essenza.Domain.Entities;
using Essenza.Domain.Enums;

namespace Essenza.Tests.Domain;

public class CatalogTests
{
    [Fact]
    public void Variants_KeepStockSeparateForEachSize()
    {
        var brand = new Brand("Supreme");
        var category = new Category("Camisetas");
        var product = new Product("Portrait", "", brand.Id, category.Id, Gender.Unisex, 100000m);
        var medium = new ProductVariant(product.Id, "M", "Negro", 5);
        var large = new ProductVariant(product.Id, "L", "Negro", 2);

        medium.UpdateStock(0);

        Assert.Equal(0, medium.Stock);
        Assert.Equal(2, large.Stock);
        Assert.Equal(product.Id, medium.ProductId);
    }

    [Fact]
    public void UpdatePrices_AppliesAndRemovesDiscount()
    {
        var product = CreateProduct();

        product.UpdatePrices(100000m, 80000m);
        Assert.Equal(80000m, product.GetCurrentPrice());

        product.UpdatePrices(100000m, null);
        Assert.Equal(100000m, product.GetCurrentPrice());
        Assert.Null(product.DiscountPrice);
    }

    [Theory]
    [InlineData(0, null)]
    [InlineData(-1, null)]
    [InlineData(100000, 0)]
    [InlineData(100000, -1)]
    [InlineData(100000, 100000)]
    [InlineData(100000, 120000)]
    public void UpdatePrices_InvalidPricesPreservePreviousValues(int price, int? discount)
    {
        var product = CreateProduct();
        product.UpdatePrices(100000m, 80000m);
        decimal? discountPrice = discount.HasValue ? (decimal)discount.Value : null;

        Assert.Throws<ArgumentOutOfRangeException>(() => product.UpdatePrices(price, discountPrice));

        Assert.Equal(100000m, product.Price);
        Assert.Equal(80000m, product.DiscountPrice);
    }

    [Fact]
    public void UpdateStock_NegativeStockPreservesPreviousValue()
    {
        var variant = new ProductVariant(Guid.NewGuid(), "M", "Negro", 5);

        Assert.Throws<ArgumentOutOfRangeException>(() => variant.UpdateStock(-1));

        Assert.Equal(5, variant.Stock);
    }

    private static Product CreateProduct()
    {
        return new Product("Portrait", "", Guid.NewGuid(), Guid.NewGuid(), Gender.Unisex, 100000m);
    }
}
