using BSDatabasePractice.db;
using Microsoft.EntityFrameworkCore;

namespace BSDatabasePractice;

public static class Calculations
{
    public static int Discount(long totalSold)
    {
        return totalSold switch
        {
            > 300_000 => 15,
            >= 50_000 => 10,
            >= 10_000 => 5,
            _ => 0
        };
    }

    public static int RequiredMaterial(int productTypeId, int materialTypeId, int quantity, double width, double length)
    {
        if (productTypeId <= 0 || materialTypeId <= 0 || quantity <= 0 ||
            !double.IsFinite(width) || !double.IsFinite(length) || width <= 0 || length <= 0)
            return -1;

        using var db = new PracticeDb();
        var coefficient = db.ProductTypes.Where(type => type.Id == productTypeId)
            .Select(type => (decimal?)type.Coefficient).SingleOrDefault();
        var defectRate = db.MaterialTypes.Where(type => type.Id == materialTypeId)
            .Select(type => (decimal?)type.DefectRate).SingleOrDefault();
        if (coefficient == null || defectRate == null)
            return -1;

        var area = width * length;
        if (!double.IsFinite(area) || area > (double)decimal.MaxValue)
            return -1;
        if (area < 0.0000000000000000000000000001)
            return 1;

        try
        {
            var amount = (decimal)area * coefficient.Value * quantity * (1 + defectRate.Value);
            var rounded = Math.Max(1, Math.Ceiling(amount));
            return rounded > int.MaxValue ? -1 : (int)rounded;
        }
        catch (OverflowException)
        {
            return -1;
        }
    }
}
