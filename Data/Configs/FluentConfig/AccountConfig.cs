using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasData(AccountSeed.GetAccountWallet());
            builder.Property(i => i.Title).IsRequired();
            builder.Property(i => i.AccountNumber).IsRequired().IsUnicode(true);
            builder.Property(i => i.Amount).IsRequired().HasDefaultValue(0);
            builder.HasOne(i => i.User).WithMany(i => i.Accounts).HasForeignKey(i => i.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.AccountType).WithMany(i=>i.Accounts).HasForeignKey(i => i.AccountTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}