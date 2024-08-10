using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class TransactionTypeConfig : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.HasData(TransactionTypeSeed.GetTransactionTypes());
            builder.Property(i => i.Title).IsRequired();
        }
    }
}
