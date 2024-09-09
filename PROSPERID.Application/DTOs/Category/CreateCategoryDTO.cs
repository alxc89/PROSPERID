namespace PROSPERID.Application.DTOs.Category;

public class CreateCategoryDTO(string name)
{
    /// <summary>
    /// Nome da Categoria
    /// </summary>
    /// <example>Educação</example>
    public string Name { get; set; } = name;
}