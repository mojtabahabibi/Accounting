using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasData(ItemSeed.GetItems());
            builder.Property(i => i.Name).IsRequired();
            builder.Property(i => i.Code).IsRequired();
            builder.Property(i => i.Price).HasDefaultValue(0).IsRequired();
        }
    }
}
