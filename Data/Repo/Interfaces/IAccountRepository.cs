using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IAccountRepository  : IBaseRepository<Account>
    {
        Task<Account> CreateAsync(Account account);
    }
}