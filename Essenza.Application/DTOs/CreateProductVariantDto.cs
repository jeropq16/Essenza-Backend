namespace Essenza.Application.DTOs;

public class CreateProductVariantDto
{
    public Guid ProductId { get; set; }
    public string Size { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int Stock { get; set; }
}
