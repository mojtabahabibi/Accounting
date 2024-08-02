using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface ICompanyRepository : IBaseRepo<Company>
    {
        Task<Company> AddComapnyAsync(Company entity);
    }
}
