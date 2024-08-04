using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(AccountingDbContext dbContext, ILogger<BaseRepository<Wallet>> logger) : base(dbContext, logger)
        {

        }
        public async Task<List<WalletListDto>> GetAllWalletAsync()
        {
            return await dbContext.Wallets.Include(i => i.Account).ThenInclude(i => i.AccountUser)
                .Select(i => new WalletListDto()
                {
                    AccountNumber = i.Account.AccountNumber,
                    AccountUser = i.Account.AccountUser.UserName,
                    WalletNumber = i.WalletNumber,
                    Amount = i.Amount
                }).ToListAsync();
        }

        public async Task<WalletListDto> GetByUsernameAsync(string username)
        {
            var wallet = await dbContext.Wallets.Include(i => i.Account).ThenInclude(i => i.AccountUser)
                .FirstOrDefaultAsync(i => i.Account.AccountUser.UserName.Equals(username));
            return new WalletListDto()
            {
                AccountNumber = wallet.Account.AccountNumber,
                AccountUser = wallet.Account.AccountUser.UserName,
                WalletNumber = wallet.WalletNumber,
                Amount = wallet.Amount
            };
        }
    }
}