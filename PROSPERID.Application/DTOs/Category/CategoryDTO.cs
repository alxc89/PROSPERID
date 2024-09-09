namespace PROSPERID.Application.DTOs.Category;

public class CategoryDTO(long id, string name)
{
    /// <summary>
    /// Id Categoria
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; } = id;

    /// <summary>
    /// Nome da Categoria
    /// </summary>
    /// <example>Educação</example>
    public string Name { get; set; } = name;

    public static implicit operator CategoryDTO(Core.Entities.Category category)
    {
        return new CategoryDTO(category.Id, category.Name);
    }
}