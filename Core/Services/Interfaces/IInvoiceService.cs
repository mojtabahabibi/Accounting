using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<GetAllInvoiceResponseDto> GetAllInvoiceAsync();
        Task<GetByIdInvoiceResponseDto> GetByIdInvoiceAsync(DeleteInvoiceDto dto);
        Task<BaseResponseDto<bool?>> CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<BaseResponseDto<bool?>> UpdateInvoiceAsync(UpdateInvoiceDto dto);
        Task<BaseResponseDto<bool?>> DeleteAccountAsync(DeleteInvoiceDto dto);
        Task<BaseResponseDto<bool?>> PaymentAsync(PaymentInvoiceDto dto);
        Task<BaseResponseDto<bool?>> CloseInvoiceAsync(CloseInvoiceDto dto);
        Task<BaseResponseDto<bool?>> DepositAsync(CreatePaymentDto dto);
    }
}
