using Essenza.Domain.Entities;

namespace Essenza.Domain.Interfaces;

// Las consultas por Id devuelven null si no existe la entidad.
// Las consultas de listas devuelven una lista vacía si no hay resultados.
// Cada operación de escritura debe guardar sus cambios antes de finalizar.
public interface IBrandRepository
{
    Task<Brand?> GetByIdAsync(Guid id);
    Task<List<Brand>> GetAllAsync();
    Task AddAsync(Brand brand);
}
