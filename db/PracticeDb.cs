using System.IO;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BSDatabasePractice.db;

public class PracticeDb : DbContext
{
    public DbSet<Partner> Partners => Set<Partner>();
    public DbSet<PartnerType> PartnerTypes => Set<PartnerType>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductType> ProductTypes => Set<ProductType>();
    public DbSet<MaterialType> MaterialTypes => Set<MaterialType>();
    public DbSet<Sale> Sales => Set<Sale>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var path = FindEnvFile();
        var settings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadLines(path))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith('#'))
                continue;

            var pair = line.Split('=', 2);
            if (pair.Length == 2)
                settings[pair[0].Trim()] = pair[1].Trim();
        }

        if (string.IsNullOrWhiteSpace(settings.GetValueOrDefault("PGPASSWORD")))
            throw new InvalidOperationException("Укажите пароль PostgreSQL в файле .env (PGPASSWORD).");

        if (!int.TryParse(settings.GetValueOrDefault("PGPORT", "5432"), out int port) || port is < 1 or > 65535)
            throw new InvalidOperationException("Укажите корректный порт PostgreSQL в файле .env (PGPORT).");

        var connection = new NpgsqlConnectionStringBuilder
        {
            Host = settings.GetValueOrDefault("PGHOST", "localhost"),
            Port = port,
            Database = settings.GetValueOrDefault("PGDATABASE", "master_pol"),
            Username = settings.GetValueOrDefault("PGUSER", "postgres"),
            Password = settings["PGPASSWORD"],
            Timeout = 5
        };
        options.UseNpgsql(connection.ConnectionString);
    }

    private static string FindEnvFile()
    {
        foreach (var folder in new[] { AppContext.BaseDirectory, Environment.CurrentDirectory })
        {
            for (var directory = new DirectoryInfo(folder); directory != null; directory = directory.Parent)
            {
                var path = Path.Combine(directory.FullName, ".env");
                if (File.Exists(path))
                    return path;
            }
        }

        throw new InvalidOperationException("Создайте файл .env на основе .env.example и укажите настройки PostgreSQL.");
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<PartnerType>().HasIndex(type => type.Name).IsUnique();
        model.Entity<ProductType>().HasIndex(type => type.Name).IsUnique();
        model.Entity<MaterialType>().HasIndex(type => type.Name).IsUnique();
        model.Entity<Product>().HasIndex(product => product.Article).IsUnique();

        model.Entity<Partner>().Property(partner => partner.Id).HasIdentityOptions(startValue: 6);
        model.Entity<Product>().Property(product => product.Id).HasIdentityOptions(startValue: 6);
        model.Entity<PartnerType>().Property(type => type.Id).HasIdentityOptions(startValue: 5);
        model.Entity<ProductType>().Property(type => type.Id).HasIdentityOptions(startValue: 5);
        model.Entity<MaterialType>().Property(type => type.Id).HasIdentityOptions(startValue: 6);
        model.Entity<Sale>().Property(sale => sale.Id).HasIdentityOptions(startValue: 17);

        model.Entity<Partner>().HasOne(partner => partner.Type).WithMany().OnDelete(DeleteBehavior.Restrict);
        model.Entity<Product>().HasOne(product => product.Type).WithMany().OnDelete(DeleteBehavior.Restrict);
        model.Entity<Partner>().HasMany(partner => partner.Sales).WithOne().OnDelete(DeleteBehavior.Restrict);
        model.Entity<Sale>().HasOne(sale => sale.Product).WithMany().OnDelete(DeleteBehavior.Restrict);

        MockData.AddTo(model);
    }
}
