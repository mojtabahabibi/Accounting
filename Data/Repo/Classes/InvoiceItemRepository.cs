using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class InvoiceItemRepository : BaseRepository<InvoiceItem>, IInvoiceItemRepository
    {
        public InvoiceItemRepository(AccountingDbContext dbContext, ILogger<BaseRepository<InvoiceItem>> logger) : base(dbContext, logger)
        {

        }
        public async Task<InvoiceItem> CreateInvoiceItemAsync(InvoiceItem entity)
        {
            logger.LogInformation("InvoiceItemRepository CreateInvoiceItemAsync was called for ");
            try
            {
                await dbContext.InvoiceItems.AddAsync(entity);

                var invoice = await dbContext.Invoices.FindAsync(entity.InvoiceId);
                if (invoice != null)
                {
                    invoice.Price = invoice.Price + (entity.Price * entity.Count);
                    invoice.Off = invoice.Off + entity.Off;
                    invoice.TotalPrice = invoice.Price - invoice.Off;
                }
                await dbContext.SaveChangesAsync();
                logger.LogInformation("InvoiceItemRepository CreateInvoiceItemAsync was Done for ");
                return entity;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemRepository CreateInvoiceItemAsync was Failed for ");
                throw;
            }
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
        public async Task<InvoiceStatus> InvoiceStatus(long invoiceId)
        {
            logger.LogInformation("InvoiceItemRepository InvoiceStatus was called for ");
            try
            {
                var result = await dbContext.Invoices.FindAsync(invoiceId);
                logger.LogInformation("InvoiceItemRepository InvoiceStatus was Done for ");
                return result.Status;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemRepository InvoiceStatus was Failed for ");
                throw;
            }
        }
    }
}
