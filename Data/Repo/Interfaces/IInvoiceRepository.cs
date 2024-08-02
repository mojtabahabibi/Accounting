using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IInvoiceRepository : IBaseRepo<Invoice>
    {
        Task<List<InvoiceListDto>> GetAllInvoiceAsync();
        Task<InvoiceListDto> GetByIdInvoiceAsync(long invoiceId);
        Task<PaymentResult> PaymentAsync(long invoiceId);
    }
}
