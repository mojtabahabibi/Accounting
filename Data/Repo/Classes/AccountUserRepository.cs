using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class AccountUserRepository : BaseRepository<AccountUser>, IAccountUserRepository
    {
        public AccountUserRepository(AccountingDbContext dbContext, ILogger<BaseRepository<AccountUser>> logger) : base(dbContext, logger)
        {
        }
        public async Task<AccountUser> CreateUserAsync(AccountUser model)
        {
            await dbContext.AccountUsers.AddAsync(model);
            await dbContext.SaveChangesAsync();

            //create AccountWallet
            var accountWallet = new Account()
            {
                AccountTypeId = 2,
                AccountUserId = model.Id,
                AccountUser = model,
                Title = "حساب کیف پول",
                AccountNumber = "123456789"
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

            //create AccountCash
            var AccountCash = new Account()
            {
                AccountTypeId = 1,
                AccountUserId = model.Id,
                AccountUser = model,
                Title = "حساب نقدی",
                AccountNumber = "987654321"
            };
            await dbContext.Accounts.AddAsync(AccountCash);
            await dbContext.SaveChangesAsync();

            return model;
        }
    }
}
