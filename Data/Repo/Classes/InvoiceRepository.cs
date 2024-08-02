using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class InvoiceRepository : BaseRepo<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AccountingDbContext dbContext, ILogger<BaseRepo<Invoice>> logger) : base(dbContext, logger)
        {

        }
        public async Task<List<InvoiceListDto>> GetAllInvoiceAsync()
        {
            logger.LogInformation("InvoiceRepository GetAllAsync was called for ");
            try
            {
                var entity = await dbContext.Invoices.Include(i => i.InvoiceItems).ThenInclude(i => i.Item).Where(i => i.DeletedDate == null)
                    .Select(i => new InvoiceListDto
                    {
                        AccountUserId = i.AccountUserId,
                        ComapnyId = i.ComapnyId,
                        InvoiceId = i.Id,
                        Title = i.Title,
                        SerialNumber = i.SerialNumber,
                        Price = i.Price,
                        Off = i.Off,
                        TotalPrice = i.TotalPrice,
                        Date = i.Date,
                        InvoiceItems = i.InvoiceItems.Where(i => i.DeletedDate == null).Select(j => new InvoiceItemDetailsResponseDto
                        {
                            Id = j.Id,
                            ItemId = j.ItemId,
                            ItemName = j.Item.Name,
                            Count = j.Count,
                            Off = j.Off,
                            Price = j.Price,
                        }).ToList()
                    }).ToListAsync();
                logger.LogInformation("InvoiceRepository GetAllAsync was Done for ");
                return entity;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository GetAllAsync was Failed for ");
                throw;
            }
        }
        public async Task<InvoiceListDto> GetByIdInvoiceAsync(long invoiceId)
        {
            logger.LogInformation("InvoiceRepository GetByIdAsync was called for ");
            try
            {
                var entity = await dbContext.Invoices.Include(i => i.InvoiceItems).ThenInclude(i => i.Item).Where(i => i.DeletedDate == null && i.Id.Equals(invoiceId))
                    .Select(i => new InvoiceListDto
                    {
                        AccountUserId = i.AccountUserId,
                        ComapnyId = i.ComapnyId,
                        InvoiceId = i.Id,
                        Title = i.Title,
                        SerialNumber = i.SerialNumber,
                        Price = i.Price,
                        Off = i.Off,
                        TotalPrice = i.TotalPrice,
                        Date = i.Date,
                        InvoiceItems = i.InvoiceItems.Where(i => i.DeletedDate == null).Select(j => new InvoiceItemDetailsResponseDto
                        {
                            Id = j.Id,
                            ItemId = j.ItemId,
                            ItemName = j.Item.Name,
                            Count = j.Count,
                            Off = j.Off,
                            Price = j.Price
                        }).ToList()
                    }).FirstOrDefaultAsync();
                logger.LogInformation("InvoiceRepository GetByIdAsync was Done for ");
                return entity;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository GetByIdAsync was Failed for ");
                throw;
            }
        }
        public async Task<PaymentResult> PaymentAsync(long invoiceId)
        {
            var invoice = await dbContext.Invoices.Include(i => i.InvoiceItems).FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (invoice != null)
            {
                var account = await dbContext.Accounts.FirstOrDefaultAsync(i => i.AccountUserId == invoice.AccountUserId);
                if (account != null)
                {
                    var wallets = await dbContext.Wallets.FirstOrDefaultAsync(i => i.AccountId == account.Id);
                    if (wallets != null)
                    {
                        long totallPrice = invoice.InvoiceItems.Sum(i => i.Price * i.Count - i.Off);
                        if (totallPrice != 0)
                        {
                            if (wallets.Amount > totallPrice)
                            {
                                invoice.Status = true;
                                var transaction = new AccountTransaction()
                                {
                                    InvoiceId = invoiceId,
                                    Invocie = invoice,
                                    TransactionNumber = Guid.NewGuid(),
                                    Time = DateTime.Now,
                                };
                                await dbContext.AccountTransactions.AddAsync(transaction);
                                await dbContext.SaveChangesAsync();

                                long amount = wallets.Amount;
                                wallets.Amount = amount - totallPrice;

                                await dbContext.SaveChangesAsync();
                                return PaymentResult.Done;
                            }
                            else
                                return PaymentResult.DontMoney;
                        }
                        else
                            return PaymentResult.DontInvoiceItem;
                    }
                    else
                        return PaymentResult.DontWallet;
                }
                else
                    return PaymentResult.DontAccount;
            }
            else
                return PaymentResult.DontInvoice;
        }
    }
}
