using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class InvoiceItemConfig : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.Property(i => i.Name).IsRequired();
            builder.Property(i => i.Price).HasDefaultValue(0).IsRequired();
            builder.Property(i => i.Count).HasDefaultValue(0).IsRequired();
            builder.Property(i => i.Discount).HasDefaultValue(0).IsRequired();
            builder.HasOne(i => i.Invoice).WithMany(i => i.InvoiceItems).HasForeignKey(i => i.InvoiceId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
