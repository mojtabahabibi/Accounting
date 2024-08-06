using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

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

            var accountNumberList = await dbContext.Accounts.Select(i => new { AccountNumber = i.AccountNumber }).ToListAsync();

            string accountNumberCash = random.Next(99999999, 1000000000).ToString();
            string accountNumberWallet = random.Next(99999999, 1000000000).ToString();

            while (accountNumberList.Any(i => i.AccountNumber.Equals(accountNumberCash)))
            {
                accountNumberCash = random.Next(99999999, 1000000000).ToString();
            }
            while (accountNumberList.Any(i => i.AccountNumber.Equals(accountNumberWallet)))
            {
                accountNumberWallet = random.Next(99999999, 1000000000).ToString();
            }

            //create AccountCash
            var AccountCash = new Account()
            {
                AccountTypeId = 1,
                AccountUserId = model.Id,
                AccountUser = model,
                Title = "حساب نقدی",
                AccountNumber = accountNumberCash
            };
            await dbContext.Accounts.AddAsync(AccountCash);

            //create AccountWallet
            var accountWallet = new Account()
            {
                AccountTypeId = 2,
                AccountUserId = model.Id,
                AccountUser = model,
                Title = "حساب کیف پول",
                AccountNumber = accountNumberWallet
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
