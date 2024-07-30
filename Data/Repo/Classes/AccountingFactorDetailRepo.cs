using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class AccountingFactorDetailRepo : BaseRepo<AccountingFactorDetails>, IAccountingFactorDetailRepo
    {
        public AccountingFactorDetailRepo(AccountingDbContext dbContext, ILogger<BaseRepo<AccountingFactorDetails>> logger) : base(dbContext, logger)
        {
        }
    }
}