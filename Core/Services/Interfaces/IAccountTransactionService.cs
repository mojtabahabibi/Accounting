using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IAccountTransactionService
    {
        AccountTranasctionGetAllResponseDto GetAllAccountTransactionAsync();
        Task<AccountTranasctionGetAllResponseDto> GetByUsernameAsync(string username); 
        Task<AccountTranasctionGetByUsernameResponseDto> GetByTransactionNumberAsync(Guid number);
    }
}