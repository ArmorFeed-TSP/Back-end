
using ArmorFeedApi.Shared.Extensions;
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Vehicles.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Shared.Persistence.Contexts;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Shipment> Shipments;
    public DbSet<ShipmentReview> ShipmentReviews;
    public DbSet<Enterprise> Enterprises { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    

        //Shipments
        builder.Entity<Shipment>().ToTable("Shipments");
        builder.Entity<Shipment>().HasKey(s => s.Id);
        builder.Entity<Shipment>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Shipment>().Property(s => s.OriginCity).IsRequired().HasMaxLength(50);
        builder.Entity<Shipment>().Property(s => s.DestinationCity).IsRequired().HasMaxLength(50);
        builder.Entity<Shipment>().Property(s => s.PickUpDate).IsRequired();
        builder.Entity<Shipment>().Property(s => s.DeliveryDate).IsRequired();
        builder.Entity<Shipment>().Property(s => s.ShipmentStatus).IsRequired();
        
        // Shipments Relationships
        // With Enterprise
        builder.Entity<Shipment>().HasOne(s => s.Enterprise);
        // With Costumer
        builder.Entity<Shipment>().HasOne(s => s.Customer);
        
        // Shipments Review
        builder.Entity<ShipmentReview>().ToTable("ShipmentReviews");
        builder.Entity<ShipmentReview>().HasKey(s => s.Id);
        builder.Entity<Shipment>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ShipmentReview>().Property(s => s.ReviewDate).IsRequired();
        builder.Entity<ShipmentReview>().Property(s => s.Text).IsRequired();
        builder.Entity<ShipmentReview>().Property(s => s.Score).IsRequired();
        
        // Shipment Reviews Relationships
        // With Shipment
        builder.Entity<ShipmentReview>().HasOne(s => s.Shipment);
        
        //Vehicles
        builder.Entity<Vehicle>().ToTable("Vehicles");
        builder.Entity<Vehicle>().HasKey(p => p.Id);
        builder.Entity<Vehicle>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(p => p.Brand).IsRequired().HasMaxLength(120);
        builder.Entity<Vehicle>().Property(p => p.Year).IsRequired().HasMaxLength(120);
        builder.Entity<Vehicle>().Property(p => p.Model).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.LicensePlate).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.VehicleType).IsRequired();
        
        //Relationships
        builder.Entity<Enterprise>()
            .HasMany(p => p.Vehicles)
            .WithOne(p => p.Enterprise)
            .HasForeignKey(p => p.EnterpriseId);
        
        //Enterprise
        builder.Entity<Enterprise>().ToTable("Enterprises");
        builder.Entity<Enterprise>().HasKey(s => s.Id);
        builder.Entity<Enterprise>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Enterprise>().Property(s => s.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Enterprise>().Property(s => s.Ruc).IsRequired().HasMaxLength(50);
        builder.Entity<Enterprise>().Property(s => s.PhoneNumber).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.Email).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.PriceBase).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.FactorWeight).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.ShippingTime).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.Score).IsRequired();
        builder.Entity<Enterprise>().Property(s => s.Photo).IsRequired();


        //Apply Snake Case Naming Conventios
        builder.UseSnakeCaseNamingConvention();
    }
}