namespace PROSPERID.Application.DTOs.Category;
public class CategoryDTO
{
    public CategoryDTO(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static implicit operator CategoryDTO(Core.Entities.Category category)
    {
        return new CategoryDTO(category.Id, category.Name);
    }
}