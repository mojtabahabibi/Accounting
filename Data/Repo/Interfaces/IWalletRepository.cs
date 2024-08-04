using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<List<WalletListDto>> GetAllWalletAsync();
        Task<WalletListDto> GetByUsernameAsync(string username);
    }
}