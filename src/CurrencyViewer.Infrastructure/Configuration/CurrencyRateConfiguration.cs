using CurrencyViewer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyViewer.Infrastructure.Configuration
{
    public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.HasIndex(p => new { p.Date, p.CurrencyType }).IsUnique();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.CurrencyType)
                .IsRequired();

            builder.Property(x => x.Value)
              .IsRequired();

            builder.Property(x => x.ReceivedAt)
                .IsRequired();
        }
    }
}
