using Microsoft.EntityFrameworkCore;
using PrometheusMVPProvider.Data;

namespace PrometheusMVPProvider;
internal class JunkDbContext : DbContext
{
    public DbSet<Junkyard> Junkyards { get; set; }
    public DbSet<Junk> Junks { get; set; }

    const string ConnectionString = "Server=localhost\\SQLExpress;Database=junkyard;TrustServerCertificate=True;User=sa;Password=MPTzQ2U#6uUn3nu8";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlServer(ConnectionString,
            builder => { }  // builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
        );
    }
}
