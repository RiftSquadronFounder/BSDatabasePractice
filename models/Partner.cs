using System.ComponentModel.DataAnnotations;

namespace BSDatabasePractice;

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
