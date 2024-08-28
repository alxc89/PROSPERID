namespace PROSPERID.Core.Entities;

public class Category : Entity
{
    public string Name { get; set; } = null!;
    public ICollection<Transaction>? Transactions { get; set; }

    public Category(string name)
    {
        Name = name;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name)
    {
        Name = name;
        UpdatedAt = DateTime.Now;
    }
}

