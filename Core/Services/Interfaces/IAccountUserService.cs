using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IAccountUserService
    {
        Task<BaseResponseDto<bool?>> CreateAccountUserAsync(CreateAccountUserDto dto);
    }
}