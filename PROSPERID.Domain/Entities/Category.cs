namespace PROSPERID.Domain.Entities;

public class Category : Entity
{
    public string Name { get; set; }
    public List<FinancialMovement>? FinancialMovements { get; set; }

    public Category(string name) => Name = name;

    public void Update(string name)
    {
        Name = name;
    }
}

