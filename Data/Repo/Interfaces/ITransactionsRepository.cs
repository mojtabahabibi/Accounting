using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface ITransactionsRepository : IBaseRepository<Transactions>
    {
        Task<List<TransactionsListDto>> GetAllTransactionsAsync();
        Task<List<TransactionsListDto>> GetByAccountIdTransactionAsync(long accountid);
        Task<List<TransactionsListDto>> GetByUserNameAsync(string username);
        Task<List<TransactionsListDto>> GetByTransactionNumberAsync(string number);
        Task UpdateRefrenceId(Transactions transaction1, Transactions transaction2);
    }
}