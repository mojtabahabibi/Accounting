using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IInvoiceItemRepository : IBaseRepo<InvoiceItem>
    {
        Task<bool> UpdateInvoiceItemAsync(InvoiceItem entity);
    }
}