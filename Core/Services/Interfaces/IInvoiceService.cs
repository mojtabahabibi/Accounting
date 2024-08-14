using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<GetAllInvoiceResponseDto> GetAllInvoiceAsync();
        Task<GetByIdInvoiceResponseDto> GetByIdInvoiceAsync(InvoiceIdDto dto);
        Task<BaseResponseDto<bool?>> CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<BaseResponseDto<bool?>> UpdateInvoiceAsync(UpdateInvoiceDto dto);
        Task<BaseResponseDto<bool?>> DeleteAccountAsync(InvoiceIdDto dto);
        Task<BaseResponseDto<bool?>> CloseInvoiceAsync(InvoiceIdDto dto);
        Task<BaseResponseDto<bool?>> CancelAsync(InvoiceIdDto dto);
        Task<BaseResponseDto<bool?>> ReturnAsync(InvoiceIdDto dto);
        Task<BaseResponseDto<bool?>> BuyChargeAsync(BuyChargeDto dto);
        Task<BaseResponseDto<bool?>> PaymentChargeAsync(PaymentChargeDto dto);
        Task<InvoiceStatusListResponseDto> InvoiceStatusListAsync(InvoiceIdDto dto);
    }
}