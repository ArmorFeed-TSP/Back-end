using ArmorFeedApi.Customers.Domain.Models;
using ArmorFeedApi.Enterprises.Domain.Models;
using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Security.Domain.Models;
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

    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Enterprise> Enterprises { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Customer> Customers{ get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Payments
        //Payments
        builder.Entity<Payment>().ToTable("Payments");
        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payment>().Property(p => p.Amount).IsRequired();
        builder.Entity<Payment>().Property(p => p.Currency).IsRequired().HasMaxLength(20);

        #endregion

        #region Enterprises
        
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
        //Customers
        builder.Entity<Customer>().ToTable("Customers");
        builder.Entity<Customer>().HasKey(s => s.Id);
        builder.Entity<Customer>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Customer>().Property(s => s.Email).IsRequired();
        builder.Entity<Customer>().Property(s => s.Name).IsRequired();
        builder.Entity<Customer>().Property(s => s.PhoneNumber).IsRequired();
        builder.Entity<Customer>().Property(s => s.Ruc).IsRequired();
        builder.Entity<Customer>().Property(s => s.SubscriptionPlan).IsRequired();

        //vehicles
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
        
        #endregion

        #region Shipments

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




        #endregion

        #region Users

        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(p => p.Photo);
        builder.Entity<User>().Property(p => p.Ruc).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(p => p.PhoneNumber).IsRequired().HasMaxLength(9);
        builder.Entity<User>().Property(p => p.Description).IsRequired();
        builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(100);
        
        #endregion
        //Apply Snake Case Naming Conventios
        builder.UseSnakeCaseNamingConvention();
    }
}