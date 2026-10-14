using Essenza.Domain.Enums;

namespace Essenza.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid BrandId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Gender Gender { get; private set; }
    public decimal Price { get; private set; }
    public decimal? DiscountPrice { get; private set; }
    public bool IsNew { get; set; }
    public bool IsActive { get; set; } = true;

    public Product(string name, string description, Guid brandId,
        Guid categoryId, Gender gender, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(description);

        if (brandId == Guid.Empty)
            throw new ArgumentException("La marca es obligatoria.", nameof(brandId));

        if (categoryId == Guid.Empty)
            throw new ArgumentException("La categoría es obligatoria.", nameof(categoryId));

        if (!Enum.IsDefined(gender))
            throw new ArgumentException("El género no es válido.", nameof(gender));

        Id = Guid.NewGuid();
        Name = name.Trim();
        Description = description.Trim();
        BrandId = brandId;
        CategoryId = categoryId;
        Gender = gender;
        UpdatePrices(price, null);
    }

    // Se actualizan juntos para no dejar un descuento mayor que el precio normal.
    public void UpdatePrices(decimal price, decimal? discountPrice)
    {
        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "El precio debe ser mayor que cero.");

        if (discountPrice.HasValue && (discountPrice.Value <= 0 || discountPrice.Value >= price))
            throw new ArgumentOutOfRangeException(nameof(discountPrice),
                "El precio con descuento debe ser mayor que cero y menor que el precio normal.");

        Price = price;
        DiscountPrice = discountPrice;
    }

    public decimal GetCurrentPrice()
    {
        return DiscountPrice ?? Price;
    }
}
