using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class CompanyRepo : BaseRepo<Company>, ICompanyRepo
    {
        public CompanyRepo(AccountingDbContext dbContext, ILogger<BaseRepo<Company>> logger) : base(dbContext, logger)
        {
        }
    }
}