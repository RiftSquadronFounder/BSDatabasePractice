using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BSDatabasePractice;

public class ProductType
{
    public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; } = "";
    [Precision(10, 4)] public decimal Coefficient { get; set; }
}
