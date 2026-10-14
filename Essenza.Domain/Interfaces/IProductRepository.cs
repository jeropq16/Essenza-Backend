using Essenza.Domain.Entities;

namespace Essenza.Domain.Interfaces;

// Las consultas por Id devuelven null si no existe la entidad.
// Las consultas de listas devuelven una lista vacía si no hay resultados.
// Cada operación de escritura debe guardar sus cambios antes de finalizar.
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);

    // Guarda cambios de precio, descuento y estado del producto.
    Task UpdateAsync(Product product);
}
