using ArmorFeedApi.Payments.Domain.Model;
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
        
        //Apply Snake Case Naming Conventios
        builder.UseSnakeCaseNamingConvention();
    }
}