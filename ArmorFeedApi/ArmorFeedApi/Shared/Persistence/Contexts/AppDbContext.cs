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

    public DbSet<Shipment> Shipments;
    public DbSet<ShipmentReview> ShipmentReviews;
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Shipments
        base.OnModelCreating(builder);
        //Transactions
        builder.Entity<Transaction>().ToTable("Transactions");
        builder.Entity<Transaction>().HasKey(p => p.Id);
        builder.Entity<Transaction>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Transaction>().Property(p => p.Amount).IsRequired();
        builder.Entity<Transaction>().Property(p => p.Currency).IsRequired().HasMaxLength(20);
        
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
        
        // With Transaction
        builder.Entity<Shipment>().HasMany(s => s.Transactions).WithOne(p=>p.Shipment).HasForeignKey(p=>p.ShipmentId);

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