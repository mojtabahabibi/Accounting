using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Task<List<InvoiceListDto>> GetAllInvoiceAsync();
        Task<InvoiceListDto> GetByIdInvoiceAsync(long invoiceId);
        Task<bool> DeleteInvoiceAsync(long invoiceId);
        Task<bool> CloseInvoiceAsync(long invoiceId);
        Task<bool> ReturnedInvoiceAsync(long invoiceId);
        Task<bool> BuyChargeAsync(BuyChargeDto model);
        Task<CancelInvoiceResult> CancelInvoiceAsync(long invoiceId);
        Task<PaymentResult> PaymentChargeAsync(PaymentChargeDto model);
        Task<InvoiceStatusListDto> InvoiceStatusListAsync(long invoiceId);
        Task ChangeStatus(InvoiceX model);
    }
}