using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Shipments.Persistence.Contexts;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Shipment> Shipments;
    public DbSet<ShipmentReview> ShipmentReviews;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Shipments
        base.OnModelCreating(builder);
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
        
        builder.UseSnakeCaseNamingConvention();
    }
}