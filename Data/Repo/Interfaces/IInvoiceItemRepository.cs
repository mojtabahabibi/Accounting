using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IInvoiceItemRepository : IBaseRepository<InvoiceItem>
    {
        Task<InvoiceItem> CreateInvoiceItemAsync(InvoiceItem entity);
        Task<bool> UpdateInvoiceItemAsync(InvoiceItem entity);
        Task<InvoiceStatus> InvoiceStatus(long invoiceId);
    }
}