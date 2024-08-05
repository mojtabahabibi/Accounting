using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<BaseResponseDto<bool?>> AddPaymentAsync(CreatePaymentDto dto);
        Task<BaseResponseDto<bool?>> DepositAsync(CreatePaymentDto dto);
        Task<BaseResponseDto<bool?>> TransferAsync(TransferDto dto);
        Task<BaseResponseDto<bool?>> PaymentAsync(PaymentInvoiceDto dto);
    }
}
