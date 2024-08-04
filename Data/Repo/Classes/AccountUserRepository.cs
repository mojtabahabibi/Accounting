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
    }
}
