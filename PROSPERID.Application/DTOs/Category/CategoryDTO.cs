namespace PROSPERID.Application.DTOs.Category;

public class CategoryDTO(long id, string name)
{
    public long Id { get; set; } = id;
    public string Name { get; set; } = name;

    public static implicit operator CategoryDTO(Core.Entities.Category category)
    {
        return new CategoryDTO(category.Id, category.Name);
    }
}