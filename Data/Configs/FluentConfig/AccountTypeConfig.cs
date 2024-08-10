using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class AccountTypeConfig : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.HasData(AccountTypeSeed.GetAccountTypes());
            builder.Property(i => i.Type).IsRequired();
        }
    }
}
