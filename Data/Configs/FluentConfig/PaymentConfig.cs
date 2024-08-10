using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(i => i.Price).HasDefaultValue(0).IsRequired();
            builder.HasOne(i => i.AccountTransaction).WithOne(i => i.Payment).HasForeignKey<Payment>(i => i.AccountTransactionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.Account).WithMany(i => i.Payments).HasForeignKey(i => i.AccountId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
