using System.ComponentModel.DataAnnotations;

namespace BSDatabasePractice;

public class PartnerType
{
    public int Id { get; set; }
    [MaxLength(30)] public string Name { get; set; } = "";
}
