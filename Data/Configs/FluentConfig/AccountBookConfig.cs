using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class AccountBookConfig : IEntityTypeConfiguration<AccountBook>
    {
        public void Configure(EntityTypeBuilder<AccountBook> builder)
        {
            builder.Property(i => i.Amount).IsRequired().HasDefaultValue(0);
            builder.HasOne(i => i.AccountTransaction).WithMany(i=>i.AccountBooks).HasForeignKey(i => i.AccountTransactionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.Account).WithMany(i=>i.AccountBooks).HasForeignKey(i => i.AccountId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}