namespace Essenza.Application.DTOs;

public class CreateProductImageDto
{
    public Guid ProductId { get; set; }
    public string Url { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}
