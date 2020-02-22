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
    }
}
