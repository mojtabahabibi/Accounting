using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Task<List<InvoiceListDto>> GetAllInvoiceAsync();
        Task<InvoiceListDto> GetByIdInvoiceAsync(long invoiceId);
        Task<PaymentResult> PaymentAsync(long invoiceId);
        Task<bool> DeleteInvoiceAsync(long id);
        Task<bool> CloseInvoice(long invoiceId);
        Task<bool> DepositAsync(Payment payment);
        Task<CancelInvoiceResult> CancelInvoiceAsync(long invoiceId);
        Task<bool> ReturnedInvoiceAsync(long invoiceId);
        Task<bool> BuyChargeAsync(BuyChargeDto model);
        Task<PaymentResult> PaymentChargeAsync(PaymentChargeDto model);
    }
}