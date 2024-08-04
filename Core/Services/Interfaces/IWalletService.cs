using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IWalletService
    {
        Task<WalletGetAllResponseDto> GetAllWallet();
        Task<WalletGetByUsernameResponseDto> GetByUsername(WalletGetByusernameDto dto);
    }
}
