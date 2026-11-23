using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data;

public class HotelListingDbContext : IdentityDbContext<ApplicationUser>
{
    public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : base(options)
    {
    }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<APIKey> APIKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<APIKey>(b => b.HasIndex(k => k.Key).IsUnique());
    }
}


