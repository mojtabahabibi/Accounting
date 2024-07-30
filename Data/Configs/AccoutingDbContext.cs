using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Configs
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountingDocument> Documents { get; set; }
        public DbSet<AccountingFactor> Factors { get; set; }
        public DbSet<AccountingFactorDetails> FactorDetails { get; set; }
        public DbSet<AccountingFinancialYear> FinancialYears { get; set; }
    }
}