using Core.Entities;

public class OrderDTO
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public decimal Freight { get; set; }
    public string ShipAddress { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}