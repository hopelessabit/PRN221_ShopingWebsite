using Core.Entities;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}