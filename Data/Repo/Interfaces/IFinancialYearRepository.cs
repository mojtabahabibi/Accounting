using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IFinancialYearRepository : IBaseRepository<FinancialYear>
    {
        Task<FinancialYearActiveResult> SetActive(BaseFinancialYearIdDto dto);
        Task<bool> SetClose(BaseFinancialYearIdDto dto);
    }
}