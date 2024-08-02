using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IItemService
    {
        Task<GetAllItemResponseDto> GetAllItemAsync();
        Task<BaseResponseDto<bool?>> CreateItemAsync(BaseItemDto dto);
    }
}
