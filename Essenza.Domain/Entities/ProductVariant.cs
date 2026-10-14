namespace Essenza.Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string Size { get; private set; }
    public string Color { get; private set; }
    public int Stock { get; private set; }

    public ProductVariant(Guid productId, string size, string color, int stock)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("El producto es obligatorio.", nameof(productId));

        ArgumentException.ThrowIfNullOrWhiteSpace(size);
        ArgumentException.ThrowIfNullOrWhiteSpace(color);

        Id = Guid.NewGuid();
        ProductId = productId;
        Size = size.Trim();
        Color = color.Trim();
        UpdateStock(stock);
    }

    public void UpdateStock(int stock)
    {
        if (stock < 0)
            throw new ArgumentOutOfRangeException(nameof(stock), "Las existencias no pueden ser negativas.");

        Stock = stock;
    }
}
