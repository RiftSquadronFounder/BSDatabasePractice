using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BSDatabasePractice;

public class Product
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType Type { get; set; } = null!;
    [MaxLength(200)] public string Name { get; set; } = "";
    [MaxLength(30)] public string Article { get; set; } = "";
    [Precision(12, 2)] public decimal MinimumPrice { get; set; }
}
