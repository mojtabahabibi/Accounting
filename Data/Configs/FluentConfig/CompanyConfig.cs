using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoBar.Accounting.Data.Configs.FluentConfig
{
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(i => i.Name).IsRequired();
            builder.Property(i => i.Economicalnumber).IsRequired().IsUnicode();
            builder.HasOne(i => i.User).WithOne(i => i.Company).HasForeignKey<Company>(i => i.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
