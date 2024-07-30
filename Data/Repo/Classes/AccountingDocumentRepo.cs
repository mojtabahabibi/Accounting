using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class AccountingDocumentRepo : BaseRepo<AccountingDocument>, IAccountingDocumentRepo
    {
        public AccountingDocumentRepo(AccountingDbContext dbContext, ILogger<BaseRepo<AccountingDocument>> logger) : base(dbContext, logger)
        {
        }
    }
}