namespace Essenza.Domain.Entities;

public class Brand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public Brand(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Id = Guid.NewGuid();
        Name = name.Trim();
    }
}
