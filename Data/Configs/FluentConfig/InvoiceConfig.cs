using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(i => i.Price).HasDefaultValue(0).IsRequired();
            builder.Property(i => i.Off).HasDefaultValue(0).IsRequired();
            builder.Property(i => i.TotalPrice).HasDefaultValue(0).IsRequired();
            builder.HasOne(i => i.AccountUser).WithMany(i => i.Invoices).HasForeignKey(i => i.AccountUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
