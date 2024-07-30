using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class AccountRepo : BaseRepo<Account>, IAccountRepo
    {
        public AccountRepo(AccountingDbContext dbContext, ILogger<BaseRepo<Account>> logger) : base(dbContext, logger)
        {
        }
    }
}