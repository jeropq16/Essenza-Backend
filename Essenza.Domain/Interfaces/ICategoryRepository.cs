using Essenza.Domain.Entities;

namespace Essenza.Domain.Interfaces;

// Las consultas por Id devuelven null si no existe la entidad.
// Las consultas de listas devuelven una lista vacía si no hay resultados.
// Cada operación de escritura debe guardar sus cambios antes de finalizar.
public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id);
    Task<List<Category>> GetAllAsync();
    Task AddAsync(Category category);
}
