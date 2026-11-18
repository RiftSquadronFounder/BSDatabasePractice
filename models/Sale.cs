namespace BSDatabasePractice;

public class Sale
{
    public int Id { get; set; }
    public int PartnerId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public DateOnly SaleDate { get; set; }
}
