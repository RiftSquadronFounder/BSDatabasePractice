using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BSDatabasePractice;

public class MaterialType
{
    public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; } = "";
    [Precision(10, 6)] public decimal DefectRate { get; set; }
}
