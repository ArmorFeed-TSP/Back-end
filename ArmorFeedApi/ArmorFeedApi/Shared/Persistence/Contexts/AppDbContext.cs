﻿using ArmorFeedApi.Comments.Domain.Models;
using ArmorFeedApi.Customers.Domain.Models;
using ArmorFeedApi.Enterprises.Domain.Models;
using ArmorFeedApi.Notifications.Domain.Models;
using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Shared.Extensions;
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Vehicles.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

    public DbSet<Customer> Customers{ get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ShipmentDriver.Domain.Models.ShipmentDriver?> ShipmentDrivers { get; set; }

    public void CreateSequence()
    {
        bool tablaExiste = Database.GetDbConnection().GetSchema("Tables").Rows.Cast<DataRow>()
            .Any(row => row["TABLE_NAME"].ToString() == "sequence");

        if (!tablaExiste)
        {

            string createTableQuery = @"
                CREATE TABLE armorfeed.sequence (
                    nombre VARCHAR(255) PRIMARY KEY,
                    valor INT
                );";

            Database.ExecuteSqlRaw(createTableQuery);

            string insertRowQuery = @"
            INSERT INTO armorfeed.sequence (nombre, valor)
            SELECT 'mi_secuencia', 0
            WHERE NOT EXISTS (
                SELECT 0 FROM armorfeed.sequence WHERE nombre = 'mi_secuencia'
            );";

            Database.ExecuteSqlRaw(insertRowQuery);
        }
    }


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

        builder.Entity<Payment>().HasOne(s => s.Shipment);
        #endregion

        #region Customers

        builder.Entity<Customer>().ToTable("Customers");
        builder.Entity<Customer>().HasKey(p => p.Id);
        builder.Entity<Customer>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Customer>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Customer>().Property(p => p.Photo).HasColumnType("longtext");
        builder.Entity<Customer>().Property(p => p.Ruc).IsRequired().HasMaxLength(50);
        builder.Entity<Customer>().Property(p => p.PhoneNumber).IsRequired().HasMaxLength(9);
        builder.Entity<Customer>().Property(p => p.Description).IsRequired();
        builder.Entity<Customer>().Property(p => p.Email).IsRequired().HasMaxLength(100);
        builder.Entity<Customer>().Property(p => p.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<Customer>().Property(p => p.SubscriptionPlan).IsRequired();

        #endregion

        #region Vehicles
        //vehicles
        builder.Entity<Vehicle>().ToTable("Vehicles");
        builder.Entity<Vehicle>().HasKey(p => p.Id);
        builder.Entity<Vehicle>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(p => p.Brand).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.Year).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.Model).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.LicensePlate).IsRequired();
        builder.Entity<Vehicle>().Property(p => p.Image).IsRequired().HasColumnType("longtext");
        builder.Entity<Vehicle>().Property(p => p.CurrentState).IsRequired()
            .HasConversion(
                currentState => currentState.ToString(),
                currentState => (VehicleState)Enum.Parse(typeof(VehicleState), currentState)
            );

        //Relationships
        builder.Entity<Enterprise>()
            .HasMany(p => p.Vehicles)
            .WithOne(p => p.Enterprise)
            .HasForeignKey(p => p.EnterpriseId);

        #endregion

        #region Enterprises

        builder.Entity<Enterprise>().ToTable("Enterprises");
        builder.Entity<Enterprise>().HasKey(p => p.Id);
        builder.Entity<Enterprise>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Enterprise>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Enterprise>().Property(p => p.Photo).HasColumnType("longtext");
        builder.Entity<Enterprise>().Property(p => p.Ruc).IsRequired().HasMaxLength(50);
        builder.Entity<Enterprise>().Property(p => p.PhoneNumber).IsRequired().HasMaxLength(9);
        builder.Entity<Enterprise>().Property(p => p.Description).IsRequired();
        builder.Entity<Enterprise>().Property(p => p.Email).IsRequired().HasMaxLength(100);
        builder.Entity<Enterprise>().Property(p => p.PriceBase).IsRequired();
        builder.Entity<Enterprise>().Property(p => p.FactorWeight).IsRequired();
        builder.Entity<Enterprise>().Property(p => p.ShippingTime).IsRequired();
        builder.Entity<Enterprise>().Property(p => p.Score).IsRequired();

        #endregion

        #region Shipments
        //Comments
        builder.Entity<Comment>().ToTable("Comments");
        builder.Entity<Comment>().HasKey(p => p.Id);
        builder.Entity<Comment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Comment>().Property(p => p.Title).IsRequired();
        builder.Entity<Comment>().Property(p => p.Content).IsRequired();

        builder.Entity<Shipment>()
            .HasMany(p => p.Comments)
            .WithOne(p => p.Shipment)
            .HasForeignKey(p => p.ShipmentId);


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
        builder.Entity<Shipment>().Property(s => s.PackageType).IsRequired();

        // Shipments Relationships
        builder.Entity<Shipment>().HasOne(s => s.Enterprise);
        builder.Entity<Shipment>().HasOne(s => s.Customer);
        builder.Entity<Vehicle>()
            .HasOne(v => v.Shipment)
            .WithOne(s => s.Vehicle)
            .HasForeignKey<Shipment>(s => s.VehicleId)
            .IsRequired(false);


        //RelationShips


        #endregion

        builder.Entity<Notification>().ToTable("Notifications");
        builder.Entity<Notification>().HasKey(n => n.Id);
        builder.Entity<Notification>().Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(n => n.Title).IsRequired().HasMaxLength(100);
        builder.Entity<Notification>().Property(n => n.Description).IsRequired();
        builder.Entity<Notification>().Property(n => n.Sender).IsRequired()
            .HasConversion(
                sender => sender.ToString(),
                sender => (NotificationSender)Enum.Parse(typeof(NotificationSender), sender)
            );
        builder.Entity<Notification>().HasOne(n => n.Customer);
        builder.Entity<Notification>().HasOne(n => n.Enterprise);

        // ShipmentDriver
        #region ShipmentDriver

        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().ToTable("ShipmentDriver");
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().HasKey(shipmentDriver => shipmentDriver.Id);
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().Property(shipmentDriver => shipmentDriver.Id);
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().Property(shipmentDriver => shipmentDriver.Description);
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().Property(shipmentDriver => shipmentDriver.Email);
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().Property(shipmentDriver => shipmentDriver.Ruc);
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().Property(shipmentDriver => shipmentDriver.Photo);
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().Property(shipmentDriver => shipmentDriver.Name);
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().Property(shipmentDriver => shipmentDriver.PasswordHash);
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>().Property(shipmentDriver => shipmentDriver.PhoneNumber);
        builder.Entity<ShipmentDriver.Domain.Models.ShipmentDriver>()
            .HasMany(driver => driver.Shipments)
            .WithOne(shipment => shipment.ShipmentDriver)
            .HasForeignKey(shipment => shipment.ShipmentDriverId).IsRequired(false); // Nullable FK
        builder.Entity<Enterprise>()
            .HasMany(e => e.ShipmentDrivers)
            .WithOne(sd => sd.Enterprise)
            .HasForeignKey(sd => sd.EnterpriseId);
        #endregion


        //Apply Snake Case Naming Convention
        builder.UseSnakeCaseNamingConvention();
    }
}