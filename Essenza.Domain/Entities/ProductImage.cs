namespace Essenza.Domain.Entities;

public class ProductImage
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string Url { get; private set; }
    public int DisplayOrder { get; private set; }

    public ProductImage(Guid productId, string url, int displayOrder)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("El producto es obligatorio.", nameof(productId));

        ArgumentException.ThrowIfNullOrWhiteSpace(url);

        if (displayOrder < 0)
            throw new ArgumentOutOfRangeException(nameof(displayOrder), "El orden no puede ser negativo.");

        Id = Guid.NewGuid();
        ProductId = productId;
        Url = url.Trim();
        DisplayOrder = displayOrder;
    }
}
