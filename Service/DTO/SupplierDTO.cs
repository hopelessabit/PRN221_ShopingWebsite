using Core.Entities;

public class SupplierDTO
{
    public int SupplierId { get; set; }

    public string CompanyName { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}