using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.SeedData;
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
        public virtual DbSet<Item> Items { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AccountBook> AccountBooks { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountUser>().HasData(AccountUserSeed.GetAccountUser());
            modelBuilder.Entity<Account>().HasData(AccountSeed.GetAccount());
            modelBuilder.Entity<Wallet>().HasData(WalletSeed.GetWallet());
            modelBuilder.Entity<TransactionType>().HasData(TransactionTypeSeed.GetTransactionTypes());

            modelBuilder.Entity<Account>().HasOne(i=>i.AccountUser)
                .WithMany().HasForeignKey(i=>i.AccountUserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Wallet>().HasOne(i => i.Account)
               .WithMany().HasForeignKey(i => i.AccountId).OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }
    }
}