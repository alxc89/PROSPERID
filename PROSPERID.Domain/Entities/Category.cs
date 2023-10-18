namespace PROSPERID.Domain.Entities;

public class Category : Entity
{
    public string Name { get; set; }
    public List<Transaction>? Transactions { get; set; }

    public Category(string name) => Name = name;

    public void Update(string name)
    {
        Name = name;
    }
}

