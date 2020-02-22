using CurrencyViewer.Domain;
using Microsoft.EntityFrameworkCore;

namespace CurrencyViewer.Infrastructure
{
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options)
        {
        }

        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
            base.OnConfiguring(options);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CurrencyDbContext).Assembly);
        }

    }
}
