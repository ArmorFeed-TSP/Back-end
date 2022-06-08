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
        
    }


}