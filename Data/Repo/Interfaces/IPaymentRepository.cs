using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<bool> DepositAsync(Payment payment);
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<TransferResult> TransferAsync(TransferDto model);
        Task<PaymentResult> PaymentAsync(PaymentInvoiceDto model);
    }
}