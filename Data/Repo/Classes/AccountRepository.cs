using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(AccountingDbContext dbContext, ILogger<BaseRepository<Account>> logger) : base(dbContext, logger)
        {
        }
        public async Task<Account> CreateAsync(Account account)
        {
            await dbContext.AddAsync(account);
            await dbContext.SaveChangesAsync();

            var wallet = new Wallet()
            {
                Account = account,
                WalletNumber = Guid.NewGuid(),
                Amount = 0
            };
            await dbContext.Wallets.AddAsync(wallet);
            await dbContext.SaveChangesAsync();

            return account;
        }
    }
}