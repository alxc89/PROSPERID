namespace PROSPERID.Core.Entities;
public abstract class Entity
{
    protected Entity()
    {
        CreatedAt = DateTime.Now;
    }

    public long Id { get; private set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
