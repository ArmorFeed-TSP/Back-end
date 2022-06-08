using ArmorFeedApi.Shared.Extensions;
using ArmorFeedApi.Payments.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Payments.Persistence.Contexts;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //Transactions
        builder.Entity<Transaction>().ToTable("Transactions");
        builder.Entity<Transaction>().HasKey(p => p.Id);
        builder.Entity<Transaction>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Transaction>().Property(p => p.Amount).IsRequired();
        builder.Entity<Transaction>().Property(p => p.Currency).IsRequired().HasMaxLength(20);
        
        //Apply Snake Case Naming Conventios
        builder.UseSnakeCaseNamingConvention();
    }


}