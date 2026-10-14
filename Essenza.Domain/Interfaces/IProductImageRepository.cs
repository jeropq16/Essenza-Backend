using Essenza.Domain.Entities;

namespace Essenza.Domain.Interfaces;

// Las consultas por Id devuelven null si no existe la entidad.
// Las consultas de listas devuelven una lista vacía si no hay resultados.
// Cada operación de escritura debe guardar sus cambios antes de finalizar.
public interface IProductImageRepository
{
    Task<ProductImage?> GetByIdAsync(Guid id);

    // Devuelve las imágenes ordenadas por DisplayOrder, de menor a mayor.
    Task<List<ProductImage>> GetByProductIdAsync(Guid productId);
    Task AddAsync(ProductImage image);
}
