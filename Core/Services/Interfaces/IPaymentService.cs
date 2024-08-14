using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<BaseResponseDto<bool?>> DepositAsync(CreatePaymentDto dto);
        Task<BaseResponseDto<bool?>> PaymentAsync(PaymentInvoiceDto dto);
    }
}
