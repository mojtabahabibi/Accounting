using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class InvoiceItemRepository : BaseRepo<InvoiceItem>, IInvoiceItemRepository
    {
        public InvoiceItemRepository(AccountingDbContext dbContext, ILogger<BaseRepo<InvoiceItem>> logger) : base(dbContext, logger)
        {

        }
        public async Task<bool> UpdateInvoiceItemAsync(InvoiceItem entity)
        {
            try
            {
                var entityFind = await dbContext.InvoiceItems.FindAsync(entity.Id);
                if (entityFind == null)
                    throw new AccountingException("Not found ID", false, ErrorCodes.NotFound);
                else
                {
                    entity.InvoiceId = entityFind.InvoiceId;
                    dbContext.Entry(entityFind).CurrentValues.SetValues(entity);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (AccountingException ex)
            {
                throw new AccountingException(ex.Message.ToString(), false, ErrorCodes.NotFound);
            }
        }
    }
}
