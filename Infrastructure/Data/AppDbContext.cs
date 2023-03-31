using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
        // applying migrations at runtime 
        Database.Migrate();
    }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Incident> Incidents { get; set; }
}
