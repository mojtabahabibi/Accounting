using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IInvoiceItemRepository : IBaseRepository<InvoiceItem>
    {
        Task<InvoiceItem> CreateInvoiceItemAsync(InvoiceItem entity);
        Task<bool> UpdateInvoiceItemAsync(InvoiceItem entity);
        Task<bool> InvoiceStatus(long invoiceId);
    }
}