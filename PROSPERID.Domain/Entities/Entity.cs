namespace PROSPERID.Domain.Entities;
public abstract class Entity : IEquatable<Guid>
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public bool Equals(Guid id) => Id == id;

    public override int GetHashCode() => Id.GetHashCode();
}
