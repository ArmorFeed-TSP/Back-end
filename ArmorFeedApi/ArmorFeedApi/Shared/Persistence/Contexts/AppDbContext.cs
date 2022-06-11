using ArmorFeedApi.Enterprises.Domain.Models;
using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Shared.Extensions;
using ArmorFeedApi.Vehicles.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Shared.Persistence.Contexts;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Enterprise> Enterprises { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Shipments
        base.OnModelCreating(builder);
        
        //Payments
        builder.Entity<Payment>().ToTable("Payments");
        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payment>().Property(p => p.Amount).IsRequired();
        builder.Entity<Payment>().Property(p => p.Currency).IsRequired().HasMaxLength(20);

        //Enterprises
        builder.Entity<Enterprise>().ToTable("Enterprises");
        builder.Entity<Enterprise>().HasKey(s => s.Id);
        builder.Entity<Enterprise>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Enterprise>().Property(s => s.Name).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.Ruc).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.PhoneNumber).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.Email).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.PriceBase).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.FactorWeight).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.ShippingTime).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.Score).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.Photo).IsRequired();
        
        builder.Entity<Vehicle>().ToTable("Vehicles");
        builder.Entity<Vehicle>().HasKey(p => p.Id);
        builder.Entity<Vehicle>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(p => p.Brand).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.Year).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.Model).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.LicensePlate).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.VehicleType).IsRequired();
        
        //Relationships
        builder.Entity<Enterprise>()
            .HasMany(p => p.Vehicles)
            .WithOne(p => p.Enterprise)
            .HasForeignKey(p => p.EnterpriseId);

        //Apply Snake Case Naming Conventios
        builder.UseSnakeCaseNamingConvention();
    }
}