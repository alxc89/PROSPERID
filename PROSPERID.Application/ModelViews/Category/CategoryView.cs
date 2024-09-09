using Entities = PROSPERID.Core.Entities;
using PROSPERID.Application.ModelViews.Transaction;

namespace PROSPERID.Application.ModelViews.Category;

public class CategoryView(long id, string name)
{
    /// <summary>
    /// Id da Categoria.
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; } = id;

    /// <summary>
    /// Nome da Categoria
    /// </summary>
    /// <example>Mensalidade escolar</example>
    public string Name { get; set; } = name;

    public static implicit operator CategoryView(Entities.Category category)
    {
        return new CategoryView(category.Id, category.Name);
    }
}
