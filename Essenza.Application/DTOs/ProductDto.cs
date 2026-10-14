using Essenza.Domain.Enums;

namespace Essenza.Application.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public Gender Gender { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public bool IsNew { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal CurrentPrice { get; set; }
}
