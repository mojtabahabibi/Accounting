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
        Task<bool> CloseInvoice(long invoiceId);
        Task<Payment> DepositAsync(Payment payment);
    }
}