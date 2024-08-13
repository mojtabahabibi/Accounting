using EcoBar.Accounting.Data.Configs.FluentConfig;
using EcoBar.Accounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Configs
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<AccountingFinancialYear> FinancialYears { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoicePayType> InvoicePayTypes { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfig());
            modelBuilder.ApplyConfiguration(new AccountTransactionConfig());
            modelBuilder.ApplyConfiguration(new AccountTypeConfig());
            modelBuilder.ApplyConfiguration(new AccountUserConfig());
            modelBuilder.ApplyConfiguration(new InvoiceConfig());
            modelBuilder.ApplyConfiguration(new InvoiceItemConfig());
            modelBuilder.ApplyConfiguration(new InvoicePayTypeConfig());
            modelBuilder.ApplyConfiguration(new ItemConfig());
            modelBuilder.ApplyConfiguration(new PaymentConfig());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}