using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class AccountUserConfig : IEntityTypeConfiguration<AccountUser>
    {
        public void Configure(EntityTypeBuilder<AccountUser> builder)
        {
            builder.HasData(AccountUserSeed.GetAccountUser());
            builder.Property(i => i.UserName).IsRequired();
            builder.Property(i=>i.Password).IsRequired();
        }
    }
}
