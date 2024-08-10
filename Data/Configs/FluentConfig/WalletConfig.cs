using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class WalletConfig : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.Property(i=>i.WalletNumber).IsRequired();
            builder.Property(i => i.Amount).HasDefaultValue(0).IsRequired();
        }
    }
}