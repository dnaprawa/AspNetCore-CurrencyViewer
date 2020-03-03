using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CurrencyViewer.Infrastructure
{
    public class CurrencyDbContextInitializer
    {
        private readonly CurrencyDbContext _dbContext;
        public CurrencyDbContextInitializer(CurrencyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
                _dbContext.Database.Migrate();
        }
    }
}
