using Essenza.Domain.Entities;

namespace Essenza.Domain.Interfaces;

// Las consultas por Id devuelven null si no existe la entidad.
// Las consultas de listas devuelven una lista vacía si no hay resultados.
// Cada operación de escritura debe guardar sus cambios antes de finalizar.
public interface IProductVariantRepository
{
    Task<ProductVariant?> GetByIdAsync(Guid id);
    Task<List<ProductVariant>> GetByProductIdAsync(Guid productId);
    Task AddAsync(ProductVariant variant);

    // Guarda los cambios de existencias de esta talla y color.
    Task UpdateAsync(ProductVariant variant);
}
