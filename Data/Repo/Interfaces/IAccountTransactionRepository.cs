using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IAccountTransactionRepository : IBaseRepository<AccountTransaction>
    {
        Task<List<AccountTransactionListDto>> GetAllAccountTransactionAsync();
        Task<List<AccountTransactionListDto>> GetByUserNameAsync(string username);
        Task<AccountTransactionListDto> GetByTransactionNumberAsync(Guid number);
    }
}