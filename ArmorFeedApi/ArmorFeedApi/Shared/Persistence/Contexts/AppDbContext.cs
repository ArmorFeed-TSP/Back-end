using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Shared.Extensions;
using ArmorFeedApi.Shipments.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Shared.Persistence.Contexts;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
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
        builder.Entity<Shipment>().Property(s => s.Origin).IsRequired().HasMaxLength(50);
        builder.Entity<Shipment>().Property(s => s.OriginAddress).IsRequired().HasMaxLength(150);
        builder.Entity<Shipment>().Property(s => s.OriginTypeAddress).IsRequired().HasMaxLength(50);
        builder.Entity<Shipment>().Property(s => s.OriginReference).IsRequired().HasMaxLength(100);
        builder.Entity<Shipment>().Property(s => s.OriginUrbanization).IsRequired().HasMaxLength(50);
        builder.Entity<Shipment>().Property(s => s.Destiny).IsRequired().HasMaxLength(50);
        builder.Entity<Shipment>().Property(s => s.DestinyAddress).IsRequired().HasMaxLength(150);
        builder.Entity<Shipment>().Property(s => s.DestinyTypeAddress).IsRequired().HasMaxLength(50);
        builder.Entity<Shipment>().Property(s => s.DestinyReference).IsRequired().HasMaxLength(100);
        builder.Entity<Shipment>().Property(s => s.DestinyUrbanization).IsRequired().HasMaxLength(60);
        builder.Entity<Shipment>().Property(s => s.PickUpDate).IsRequired();
        builder.Entity<Shipment>().Property(s => s.DeliveryDate).IsRequired();
        builder.Entity<Shipment>().Property(s => s.Status).IsRequired();
        
        // Shipments Relationships
        builder.Entity<Shipment>().HasOne(s => s.Enterprise);
        builder.Entity<Shipment>().HasOne(s => s.Customer);

        // Shipment Reviews Relationships
        // With Shipment
        builder.Entity<ShipmentReview>().HasOne(s => s.Shipment);
        
        //Apply Snake Case Naming Conventios
        builder.UseSnakeCaseNamingConvention();
    }
}