using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponseDto<bool?>> CreateUserAsync(CreateUserDto dto);
        Task<UserListResponseDto> GetAllUser();
    }
}