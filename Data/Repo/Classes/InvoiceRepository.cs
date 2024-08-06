using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
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
                        Status = i.Status == InvoiceStatus.Open ? "فاکتور باز است" : i.Status == InvoiceStatus.Close ? "فاکتور بسته شده است" :
                        i.Status == InvoiceStatus.Cancel ? "فاکتور کنسل شده است" : i.Status == InvoiceStatus.Pay ? "فاکتور پرداخت شده است" : "فاکتور لغو شده است",
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
                        Status = i.Status == InvoiceStatus.Open ? "فاکتور باز است" : i.Status == InvoiceStatus.Close ? "فاکتور بسته شده است" :
                        i.Status == InvoiceStatus.Cancel ? "فاکتور کنسل شده است" : i.Status == InvoiceStatus.Pay ? "فاکتور پرداخت شده است" : "فاکتور لغو شده است",
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
                        Invoice = invoice,
                        TransactionTypeId = type.Id,
                        TransactionType = type,
                        TransactionNumber = Guid.NewGuid()
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
                            AccountTransactionId = transaction.Id,
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
                            AccountTransactionId = transaction.Id,
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
                    invoice.Status = InvoiceStatus.Pay;

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
                if (invoice != null && invoice.Status == InvoiceStatus.Open)
                {
                    invoice.Status = InvoiceStatus.Close;
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
        public async Task<bool> DepositAsync(Payment payment)
        {
            var accountType = await dbContext.Accounts.FindAsync(payment.AccountId);
            if (accountType != null)
            {
                if (accountType.AccountTypeId == 1)
                {
                    await dbContext.Payments.AddAsync(payment);
                    await dbContext.SaveChangesAsync();

                    var type = await dbContext.TransactionTypes.FirstOrDefaultAsync(i => i.Id == 1);
                    var transaction = new AccountTransaction()
                    {
                        PaymentId = payment.Id,
                        Payment = payment,
                        TransactionTypeId = type.Id,
                        TransactionType = type,
                        TransactionNumber = Guid.NewGuid()
                    };
                    await dbContext.AccountTransactions.AddAsync(transaction);
                    await dbContext.SaveChangesAsync();

                    var account = await dbContext.Accounts.FindAsync(payment.AccountId);
                    if (account != null)
                    {
                        var accountBook = new AccountBook()
                        {
                            AccountTransactionId = transaction.Id,
                            AccountTransaction = transaction,
                            AccountId = account.Id,
                            Account = account,
                            Amount = payment.Price
                        };
                        await dbContext.AccountBooks.AddAsync(accountBook);
                        account.Amount += payment.Price;
                    }

                    var accountCompany = await dbContext.Accounts.FirstOrDefaultAsync(i => i.AccountTypeId == 1);
                    if (accountCompany != null)
                    {
                        var accountBook = new AccountBook()
                        {
                            AccountTransactionId = transaction.Id,
                            AccountTransaction = transaction,
                            AccountId = accountCompany.Id,
                            Account = accountCompany,
                            Amount = -(payment.Price)
                        };
                        await dbContext.AccountBooks.AddAsync(accountBook);
                        accountCompany.Amount -= payment.Price;
                    }

                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<CancelInvoiceResult> CancelInvoiceAsync(long invoiceId)
        {
            var invoice = await dbContext.Invoices.FindAsync(invoiceId);
            if (invoice != null)
            {
                switch (invoice.Status)
                {
                    case InvoiceStatus.Open:
                        invoice.Status = InvoiceStatus.Cancel;
                        await dbContext.SaveChangesAsync();
                        return CancelInvoiceResult.Success;
                    case InvoiceStatus.Close:
                        return CancelInvoiceResult.CloseInvoice;
                    case InvoiceStatus.Cancel:
                        return CancelInvoiceResult.CanceledBefor;
                    case InvoiceStatus.Returen:
                        return CancelInvoiceResult.Returnd;
                    case InvoiceStatus.Pay:
                        return CancelInvoiceResult.PaymentBefor;
                }
            }
            return CancelInvoiceResult.UnFoundInvoice;
        }
        public async Task<bool> ReturnedInvoiceAsync(long invoiceId)
        {
            var invoice = await dbContext.Invoices.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.Id.Equals(invoiceId));
            if (invoice != null)
            {
                if (invoice.Status == InvoiceStatus.Pay)
                {
                    var transaction = await dbContext.AccountTransactions.FirstOrDefaultAsync(i => i.InvoiceId.Equals(invoiceId));
                    if (transaction != null)
                    {
                        long totalPrice = invoice.TotalPrice;
                        var accountCompany = await dbContext.Accounts.FirstOrDefaultAsync();
                        if (accountCompany != null)
                        {
                            accountCompany.Amount -= totalPrice;
                            var accountUser = await dbContext.Accounts.FirstOrDefaultAsync(i => i.AccountUserId.Equals(invoice.AccountUserId));
                            if (accountUser != null)
                            {
                                var new_transaction = new AccountTransaction()
                                {
                                    InvoiceId = invoiceId,
                                    Invoice = invoice,
                                    TransactionTypeId = 5,
                                    TransactionNumber = Guid.NewGuid()
                                };
                                await dbContext.AccountTransactions.AddAsync(new_transaction);
                                await dbContext.SaveChangesAsync();

                                //حساب مشتری
                                var accountbookCustomer = new AccountBook()
                                {
                                    Account = accountUser,
                                    AccountId = accountUser.Id,
                                    AccountTransaction = new_transaction,
                                    AccountTransactionId = new_transaction.Id,
                                    Amount = totalPrice
                                };

                                // حساب شرکت
                                var accountbookComapny = new AccountBook()
                                {
                                    Account = accountCompany,
                                    AccountId = accountCompany.Id,
                                    AccountTransaction = new_transaction,
                                    AccountTransactionId = new_transaction.Id,
                                    Amount = -totalPrice
                                };
                                await dbContext.AccountBooks.AddAsync(accountbookCustomer);
                                await dbContext.AccountBooks.AddAsync(accountbookComapny);

                                invoice.Status = InvoiceStatus.Pay;

                                long type = transaction.TransactionTypeId;
                                switch (type)
                                {
                                    case 2:
                                        accountUser.Amount += totalPrice;
                                        await dbContext.SaveChangesAsync();

                                        return true;
                                    case 4:
                                        var wallet = await dbContext.Wallets.FirstOrDefaultAsync(i => i.AccountId.Equals(accountUser.Id));
                                        if (wallet != null)
                                        {
                                            wallet.Amount += totalPrice;
                                            await dbContext.SaveChangesAsync();
                                        }

                                        return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}