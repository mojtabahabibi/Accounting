using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IAccountantService
    {
        Task<AccountCreateGetResponseDto> GetAllAccounts();
        Task<AccountGetByIdResponseDto> GetByIdAccounts(BaseAccountIdDto dto);
        Task<BaseResponseDto<bool?>> CreateAccountAsync(BaseAccountDto dto);
        Task<BaseResponseDto<bool?>> UpdateAccountAsync(UpdateAccountDto dto);
        Task<BaseResponseDto<bool?>> DeleteAccountAsync(BaseAccountIdDto dto);
    }
}