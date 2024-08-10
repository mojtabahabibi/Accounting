using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class InvoicePayTypeConfig : IEntityTypeConfiguration<InvoicePayType>
    {
        public void Configure(EntityTypeBuilder<InvoicePayType> builder)
        {
            builder.HasData(InvoicePayTypeSeed.GetInvoicePayTypes());
            builder.Property(i => i.Type).IsRequired();
        }
    }
}
