using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class AccountTransactionConfig : IEntityTypeConfiguration<AccountTransaction>
    {
        public void Configure(EntityTypeBuilder<AccountTransaction> builder)
        {
            builder.Property(i => i.TransactionNumber).IsRequired();
            builder.HasOne(i => i.TransactionType).WithMany(i => i.AccountTransactions).HasForeignKey(i => i.TransactionTypeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i=>i.Invoice).WithMany(i=>i.AccountTransactions).HasForeignKey(i=>i.InvoiceId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.Payment).WithMany(i => i.AccountTransactions).HasForeignKey(i => i.PaymentId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
