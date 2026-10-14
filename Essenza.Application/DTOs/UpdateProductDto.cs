namespace Essenza.Application.DTOs;

public class UpdateProductDto
{
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public bool IsNew { get; set; }
    public bool IsActive { get; set; } = true;
}
