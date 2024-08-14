using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class InvoiceXConfig : IEntityTypeConfiguration<InvoiceX>
    {
        public void Configure(EntityTypeBuilder<InvoiceX> builder)
        {
            builder.HasOne(i=>i.Invoice).WithMany(i=>i.InvoiceXes).HasForeignKey(i=>i.InvoiceId).OnDelete(DeleteBehavior.NoAction);  
        }
    }
}
