using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
using EcoBar.Accounting.Data.SeedData;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AccountingDbContext dbContext, ILogger<BaseRepository<Invoice>> logger) : base(dbContext, logger)
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
            var invoice = await dbContext.Invoices.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.Id.Equals(invoiceId));
            var accountUserId = invoice.AccountUserId;
            var wallets = await dbContext.Wallets.Include(i => i.Account).FirstOrDefaultAsync(i => i.Account.AccountUserId.Equals(accountUserId));
            long totallPrice = invoice.TotalPrice;
            if (totallPrice != 0)
            {
                if (wallets.Amount > totallPrice)
                {
                    var type = await dbContext.TransactionTypes.FirstOrDefaultAsync(i => i.Id == 2);
                    var transaction = new AccountTransaction()
                    {
                        InvoiceId = invoiceId,
                        Invocie = invoice,
                        TransactionTypeId = type.Id,
                        TransactionType = type,
                        TransactionNumber = Guid.NewGuid(),
                        Time = DateTime.Now,
                    };
                    await dbContext.AccountTransactions.AddAsync(transaction);
                    await dbContext.SaveChangesAsync();

                    // حساب مشتری 
                    var accountUser = await dbContext.Accounts.Include(i => i.AccountUser)
                        .FirstOrDefaultAsync(i => i.AccountUserId == invoice.AccountUserId);
                    if (accountUser != null)
                    {
                        var accountBook = new AccountBook()
                        {
                            TransactionId = transaction.Id,
                            AccountTransaction = transaction,
                            AccountId = accountUser.Id,
                            Account = accountUser,
                            Amount = -(invoice.TotalPrice)
                        };
                        await dbContext.AccountBooks.AddAsync(accountBook);

                        var wallet = await dbContext.Wallets.FirstOrDefaultAsync(i => i.AccountId == accountUser.Id);
                        if (wallet != null)
                            wallet.Amount -= invoice.TotalPrice;
                    }
                    // حساب شرکت
                    var accountCompany = await dbContext.Accounts.FirstOrDefaultAsync();
                    if (accountCompany != null)
                    {
                        var accountBook = new AccountBook()
                        {
                            TransactionId = transaction.Id,
                            AccountTransaction = transaction,
                            AccountId = accountCompany.Id,
                            Account = accountCompany,
                            Amount = invoice.TotalPrice
                        };
                        await dbContext.AccountBooks.AddAsync(accountBook);

                        var wallet = await dbContext.Wallets.FirstOrDefaultAsync(i => i.AccountId == accountCompany.Id);
                        if (wallet != null)
                            wallet.Amount += invoice.TotalPrice;
                    }
                    await dbContext.SaveChangesAsync();
                    return PaymentResult.Done;
                }
                else
                    return PaymentResult.DontMoney;
            }
            else
                return PaymentResult.DontInvoiceItem;
        }
        public async Task<bool> CloseInvoice(long invoiceId)
        {
            logger.LogInformation("InvoiceRepository CloseInvoice was called for ");
            try
            {
                var invoice = await dbContext.Invoices.FindAsync(invoiceId);
                if (invoice != null && invoice.Status == false)
                {
                    invoice.Status = true;
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("InvoiceRepository CloseInvoice was Done for ");
                    return true;
                }
                else
                    return false;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository CloseInvoice was Failed for ");
                throw;
            }
        }
        public async Task<Payment> DepositAsync(Payment payment)
        {
            await dbContext.Payments.AddAsync(payment);
            await dbContext.SaveChangesAsync();

            var type = await dbContext.TransactionTypes.FirstOrDefaultAsync();
            var transaction = new AccountTransaction()
            {
                PaymentId = payment.Id,
                Payment = payment,
                TransactionTypeId = type.Id,
                TransactionType = type,
                TransactionNumber = Guid.NewGuid(),
                Time = DateTime.Now
            };
            await dbContext.AccountTransactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
            // حساب مشتری 
            var account = await dbContext.Accounts.Include(i => i.AccountUser)
                .FirstOrDefaultAsync(i => i.AccountUserId == payment.AccountUserId);
            if (account != null)
            {
                var accountBook = new AccountBook()
                {
                    TransactionId = transaction.Id,
                    AccountTransaction = transaction,
                    AccountId = account.Id,
                    Account = account,
                    Amount = payment.Price
                };
                await dbContext.AccountBooks.AddAsync(accountBook);

                var wallet = await dbContext.Wallets.FirstOrDefaultAsync(i => i.AccountId == account.Id);
                if (wallet != null)
                    wallet.Amount += payment.Price;
            }
            // حساب شرکت
            var accountCompany = await dbContext.Accounts.FirstOrDefaultAsync();
            if (accountCompany != null)
            {
                var accountBook = new AccountBook()
                {
                    TransactionId = transaction.Id,
                    AccountTransaction = transaction,
                    AccountId = accountCompany.Id,
                    Account = accountCompany,
                    Amount = -(payment.Price)
                };
                await dbContext.AccountBooks.AddAsync(accountBook);

                var wallet = await dbContext.Wallets.FirstOrDefaultAsync(i => i.AccountId == accountCompany.Id);
                if (wallet != null)
                    wallet.Amount -= payment.Price;
            }
            await dbContext.SaveChangesAsync();
            return payment;
        }
    }
}
