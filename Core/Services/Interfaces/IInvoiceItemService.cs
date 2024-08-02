using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IInvoiceItemService
    {
        Task<InvioceItemGetAllResponseDto> GetAllInvioceItemAsync();
        Task<InvioceItemGetByIdResponseDto> GetByIdInvioceItemAsync(BaseInvoiceItemIdDto dto);
        Task<BaseResponseDto<bool?>> CreateInvoiceItemAsync(BaseInvoiceItemDto dto);
        Task<BaseResponseDto<bool?>> UpdateInvoiceItemAsync(UpdateInvoiceItemDto dto);
        Task<BaseResponseDto<bool?>> DeleteInvoiceItemAsync(DeleteInvoiceItemDto dto);
    }
}