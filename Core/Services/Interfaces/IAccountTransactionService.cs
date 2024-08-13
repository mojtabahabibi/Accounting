using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IAccountTransactionService
    {
        Task<AccountTranasctionGetAllResponseDto> GetAllAccountTransactionAsync();
        Task<AccountTranasctionGetByAccountIdResponseDto> GetbyAccountIdTransactionAsync(long accountid);
        Task<AccountTranasctionGetAllResponseDto> GetByUsernameAsync(string username);
        Task<AccountTranasctionGetByUsernameResponseDto> GetByTransactionNumberAsync(string number);
    }
}