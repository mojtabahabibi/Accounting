using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class AccountUserRepository : BaseRepository<AccountUser>, IAccountUserRepository
    {
        Random random = new Random();
        public AccountUserRepository(AccountingDbContext dbContext, ILogger<BaseRepository<AccountUser>> logger) : base(dbContext, logger)
        {
        }
        public async Task<AccountUser> CreateUserAsync(AccountUser model)
        {
            await dbContext.AccountUsers.AddAsync(model);
            await dbContext.SaveChangesAsync();

            //create AccountCash
            var AccountCash = new Account()
            {
                AccountTypeId = 1,
                AccountUserId = model.Id,
                AccountUser = model,
                Title = "حساب نقدی",
                AccountNumber = random.Next(9999999,100000000).ToString()
            };
            await dbContext.Accounts.AddAsync(AccountCash);

            //create AccountWallet
            var accountWallet = new Account()
            {
                AccountTypeId = 2,
                AccountUserId = model.Id,
                AccountUser = model,
                Title = "حساب کیف پول",
                AccountNumber = random.Next(9999999, 100000000).ToString()
            };
            await dbContext.Accounts.AddAsync(accountWallet);
            await dbContext.SaveChangesAsync();

            var wallet = new Wallet()
            {
                Account = accountWallet,
                AccountId = accountWallet.Id,
                WalletNumber = Guid.NewGuid(),
                Amount = 0
            };
            await dbContext.Wallets.AddAsync(wallet);
            await dbContext.SaveChangesAsync();

            return model;
        }
    }
}
