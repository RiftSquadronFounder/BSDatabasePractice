using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BSDatabasePractice;

public class PartnerType
{
    public int Id { get; set; }
    [MaxLength(30)] public string Name { get; set; } = "";
}

public class Partner
{
    public int Id { get; set; }
    public int PartnerTypeId { get; set; }
    public PartnerType Type { get; set; } = null!;
    [MaxLength(200)] public string Name { get; set; } = "";
    [MaxLength(200)] public string Director { get; set; } = "";
    [MaxLength(254)] public string Email { get; set; } = "";
    [MaxLength(30)] public string Phone { get; set; } = "";
    [MaxLength(300)] public string Address { get; set; } = "";
    [MaxLength(12)] public string? TaxNumber { get; set; }
    public int Rating { get; set; }
    public List<Sale> Sales { get; set; } = [];
}

public class ProductType
{
    public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; } = "";
    [Precision(10, 4)] public decimal Coefficient { get; set; }
}

public class MaterialType
{
    public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; } = "";
    [Precision(10, 6)] public decimal DefectRate { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType Type { get; set; } = null!;
    [MaxLength(200)] public string Name { get; set; } = "";
    [MaxLength(30)] public string Article { get; set; } = "";
    [Precision(12, 2)] public decimal MinimumPrice { get; set; }
}

public class Sale
{
    public int Id { get; set; }
    public int PartnerId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public DateOnly SaleDate { get; set; }
}
