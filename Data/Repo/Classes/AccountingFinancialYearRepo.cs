using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class AccountingFinancialYearRepo : BaseRepo<AccountingFinancialYear>, IAccountingFinancialYearRepo
    {
        public AccountingFinancialYearRepo(AccountingDbContext dbContext, ILogger<BaseRepo<AccountingFinancialYear>> logger) : base(dbContext, logger)
        {
        }
        public async Task<FinancialYearActiveResult> SetActive(BaseFinancialYearIdDto dto)
        {
            var financial = await dbContext.FinancialYears.FindAsync(dto.Id);
            if (financial.DeletedDate == null)
            {
                if (financial != null)
                {
                    if (!financial.IsClose)
                    {
                        var allFinancial = await dbContext.FinancialYears.ToListAsync();
                        allFinancial.ForEach(i => i.IsActive = false);
                        financial.IsActive = true;
                        await dbContext.SaveChangesAsync();
                        return FinancialYearActiveResult.Done;
                    }
                    else
                        return FinancialYearActiveResult.IsCloseTrue;
                }
                else
                    return FinancialYearActiveResult.NotFoundedFinancialYear;
            }
            else
                return FinancialYearActiveResult.FinancialYearDelete;
        }
        public async Task<bool> SetClose(BaseFinancialYearIdDto dto)
        {
            var financial = await dbContext.FinancialYears.FindAsync(dto.Id);
            if (financial != null && financial.DeletedDate == null)
            {
                financial.IsClose = true;
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}