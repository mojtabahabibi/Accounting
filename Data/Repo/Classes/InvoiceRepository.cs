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
                        i.Status == InvoiceStatus.Cancel ? "فاکتور کنسل شده است" : i.Status == InvoiceStatus.Pay ? "فاکتور پرداخت شده است" : i.Status == InvoiceStatus.Delete ? "فاکتور حذف شده است" : "فاکتور لغو شده است",
                        Date = i.Date,
                        InvoiceItems = i.InvoiceItems != null ? i.InvoiceItems.Where(i => i.DeletedDate == null).Select(j => new InvoiceItemDetailsResponseDto
                        {
                            Id = j.Id,
                            ItemId = j.ItemId,
                            ItemName = j.Item.Name,
                            Count = j.Count,
                            Off = j.Off,
                            Price = j.Price,
                        }).ToList() : null
                    }).AsNoTracking().ToListAsync();
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
                        i.Status == InvoiceStatus.Cancel ? "فاکتور کنسل شده است" : i.Status == InvoiceStatus.Pay ? "فاکتور پرداخت شده است" : i.Status == InvoiceStatus.Delete ? "فاکتور حذف شده است" : "فاکتور لغو شده است",
                        Date = i.Date,
                        InvoiceItems = i.InvoiceItems != null ? i.InvoiceItems.Where(i => i.DeletedDate == null).Select(j => new InvoiceItemDetailsResponseDto
                        {
                            Id = j.Id,
                            ItemId = j.ItemId,
                            ItemName = j.Item.Name,
                            Count = j.Count,
                            Off = j.Off,
                            Price = j.Price
                        }).ToList() : null
                    }).AsNoTracking().FirstOrDefaultAsync();
                logger.LogInformation("InvoiceRepository GetByIdAsync was Done for ");
                return entity;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository GetByIdAsync was Failed for ");
                throw;
            }
        }
        public async Task<bool> DeleteInvoiceAsync(long id)
        {
            logger.LogInformation("InvoiceRepository DeleteAsync was called for ");
            try
            {
                var entity = dbContext.Invoices.Find(id);
                if (entity == null) throw new AccountingException("Not found ID", false, ErrorCodes.NotFound);
                entity.DeletedDate = DateTime.Now;
                entity.Status = InvoiceStatus.Delete;
                dbContext.Attach(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("InvoiceRepository DeleteAsync was Done for ");
                return true;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository DeleteAsync was Failed for ");
                throw;
            }
        }
        public async Task<PaymentResult> PaymentAsync(long invoiceId)
        {
            Random random = new Random();
            var invoice = await dbContext.Invoices.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.Id.Equals(invoiceId));
            var accountUserId = invoice.AccountUserId;
            // var wallets = await dbContext.Wallets.Include(i => i.Account).FirstOrDefaultAsync(i => i.Account.AccountUserId.Equals(accountUserId));
            long totallPrice = invoice.TotalPrice;
            if (totallPrice != 0)
            {
                if (10 > totallPrice)
                {
                    var type = await dbContext.TransactionTypes.FirstOrDefaultAsync(i => i.Id == 2);
                    var transaction = new AccountTransaction()
                    {
                        Invoice = invoice,
                        TransactionTypeId = type.Id,
                        TransactionType = type,
                        TransactionNumber = random.Next(99999, 1000000).ToString(),
                    };
                    await dbContext.AccountTransactions.AddAsync(transaction);
                    await dbContext.SaveChangesAsync();

                    // حساب مشتری 
                    var accountUser = await dbContext.Accounts.Include(i => i.AccountUser)
                        .FirstOrDefaultAsync(i => i.AccountUserId == invoice.AccountUserId);
                    if (accountUser != null)
                    {

                        //var wallet = await dbContext.Wallets.Include(i => i.Account).FirstOrDefaultAsync(i => i.Account.Id == accountUser.Id);
                        //if (wallet != null)
                        //    wallet.Amount -= invoice.TotalPrice;
                    }
                    // حساب شرکت
                    var accountCompany = await dbContext.Accounts.FirstOrDefaultAsync();
                    if (accountCompany != null)
                    {

                        //var wallet = await dbContext.Wallets.Include(i => i.Account).FirstOrDefaultAsync(i => i.Account.Id == accountCompany.Id);
                        //if (wallet != null)
                        //    wallet.Amount += invoice.TotalPrice;
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
            Random random = new Random();
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
                        Payment = payment,
                        TransactionTypeId = type.Id,
                        TransactionType = type,
                        TransactionNumber = random.Next(99999, 1000000).ToString(),
                    };
                    await dbContext.AccountTransactions.AddAsync(transaction);
                    await dbContext.SaveChangesAsync();

                    var account = await dbContext.Accounts.FindAsync(payment.AccountId);
                    if (account != null)
                    {
                        account.Amount += payment.Price;
                    }

                    var accountCompany = await dbContext.Accounts.FirstOrDefaultAsync(i => i.AccountTypeId == 1);
                    if (accountCompany != null)
                    {
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
                    case InvoiceStatus.Delete:
                        return CancelInvoiceResult.Deleted;
                }
            }
            return CancelInvoiceResult.UnFoundInvoice;
        }
        public async Task<bool> ReturnedInvoiceAsync(long invoiceId)
        {
            Random random = new Random();
            var invoice = await dbContext.Invoices.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.Id.Equals(invoiceId));
            if (invoice != null)
            {
                if (invoice.Status == InvoiceStatus.Pay)
                {
                    invoice.Status = InvoiceStatus.Returen;
                    dbContext.Attach(invoice);

                    long price = invoice.TotalPrice;
                    long accountUserId = invoice.AccountUserId;

                    var accountWallet = await dbContext.Accounts.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.AccountUserId.Equals(accountUserId) && i.AccountTypeId == 2);
                    var accountCompany = await dbContext.Accounts.FirstAsync();

                    if (accountWallet != null && accountCompany != null)
                    {
                        accountWallet.Amount += price;
                        accountCompany.Amount -= price;

                        dbContext.Attach(accountWallet);
                        dbContext.Attach(accountCompany);

                        var walletTransaction = new AccountTransaction()
                        {
                            InvoiceId = invoice.Id,
                            Invoice = invoice,
                            TransactionTypeId = 7,
                            AccountNumber = accountWallet.AccountNumber,
                            AccountUsername = accountWallet.AccountUser?.UserName,
                            InvoiceNumber = invoice.SerialNumber,
                            TransactionNumber = random.Next(99999, 1000000).ToString(),
                            Description = "ورود " + price.ToString() + " وجه نقد به حساب کیف پول " + accountWallet.AccountNumber + " بابت مرجوع فاکتور  " + invoice.SerialNumber
                        };
                        var companyTransaction = new AccountTransaction()
                        {
                            InvoiceId = invoice.Id,
                            Invoice = invoice,
                            TransactionTypeId = 7,
                            AccountNumber = accountCompany?.AccountNumber,
                            AccountUsername = accountCompany?.AccountUser?.UserName,
                            InvoiceNumber = invoice.SerialNumber,
                            TransactionNumber = random.Next(99999, 1000000).ToString(),
                            Description = "خروج " + price.ToString() + " وجه نقد از حساب صندوق  " + accountCompany?.AccountNumber + " بابت مرجوع فاکتور " + invoice.SerialNumber
                        };
                        await dbContext.AccountTransactions.AddAsync(walletTransaction);
                        await dbContext.AccountTransactions.AddAsync(companyTransaction);

                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
        public async Task<bool> BuyChargeAsync(BuyChargeDto model)
        {
            logger.LogInformation("InvoiceRepository BuyChargeAsync was called for ");
            try
            {
                Random random = new Random();
                string serialNumber = random.Next(99999, 1000000).ToString();

                var invoice = new Invoice()
                {
                    AccountUserId = model.AccountUserId,
                    Title = "خرید شارژ",
                    SerialNumber = serialNumber,
                    Price = model.Price,
                    Off = 0,
                    TotalPrice = model.Price,
                    Date = DateTime.Now,
                };
                await dbContext.Invoices.AddAsync(invoice);
                await dbContext.SaveChangesAsync();

                var item = await dbContext.Items.FirstOrDefaultAsync();
                if (item != null)
                {
                    var invoiceItem = new InvoiceItem()
                    {
                        Invoice = invoice,
                        InvoiceId = invoice.Id,
                        Item = item,
                        ItemId = item.Id,
                        Name = "خرید شارژ",
                        Count = 1,
                        Off = 0,
                        Price = model.Price,
                    };
                    await dbContext.InvoiceItems.AddAsync(invoiceItem);
                    await dbContext.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository BuyChargeAsync was Failed for ");
                throw;
            }
        }
        public async Task<PaymentResult> PaymentChargeAsync(PaymentChargeDto model)
        {
            logger.LogInformation("InvoiceRepository BuyChargeAsync was called for ");
            try
            {
                Random random = new Random();
                var account = await dbContext.Accounts.Include(i => i.AccountUser).Where(i => i.AccountUserId.Equals(model.AccountUserId)).ToListAsync();
                if (account != null)
                {
                    var invoice = await dbContext.Invoices.Include(i => i.InvoiceItems).ThenInclude(i => i.Item).FirstOrDefaultAsync(i => i.Id.Equals(model.InvoiceId));
                    if (invoice == null)
                    {
                        return PaymentResult.DontInvoice;
                    }
                    else
                    {
                        if (invoice.Status == InvoiceStatus.Open)
                        {
                            var accountCash = account.First(i => i.AccountTypeId == 1);
                            var accountWallet = account.First(i => i.AccountTypeId == 2);
                            long price = invoice.TotalPrice;

                            if (accountCash.Amount > price)
                            {
                                accountCash.Amount -= price;
                                accountWallet.Amount += price;
                                dbContext.AttachRange(account);

                                var cashTransaction = new AccountTransaction()
                                {
                                    InvoiceId = invoice.Id,
                                    Invoice = invoice,
                                    TransactionTypeId = 2,
                                    AccountNumber = accountCash.AccountNumber,
                                    AccountUsername = accountCash.AccountUser?.UserName,
                                    InvoiceNumber = invoice.SerialNumber,
                                    TransactionNumber = random.Next(99999, 1000000).ToString(),
                                    Description = "خروج " + price.ToString() + " وجه نقد از حساب نقدی " + accountCash.AccountNumber + " به حساب کیف پول " + accountWallet?.AccountNumber
                                };
                                var walletTransaction = new AccountTransaction()
                                {
                                    InvoiceId = invoice.Id,
                                    Invoice = invoice,
                                    TransactionTypeId = 3,
                                    AccountNumber = accountWallet?.AccountNumber,
                                    AccountUsername = accountWallet?.AccountUser?.UserName,
                                    InvoiceNumber = invoice.SerialNumber,
                                    TransactionNumber = random.Next(99999, 1000000).ToString(),
                                    Description = "ورود " + price.ToString() + " وجه نقد به حساب کیف پول " + accountWallet?.AccountNumber + " از حساب نقدی " + accountCash?.AccountNumber
                                };
                                await dbContext.AccountTransactions.AddAsync(cashTransaction);
                                await dbContext.AccountTransactions.AddAsync(walletTransaction);

                                invoice.Status = InvoiceStatus.Pay;
                                dbContext.Attach(invoice);

                                await dbContext.SaveChangesAsync();
                                return PaymentResult.Done;
                            }
                            return PaymentResult.DontMoney;
                        }
                        return PaymentResult.StatusInvoice;
                    }
                }
                return PaymentResult.DontAccount;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository BuyChargeAsync was Failed for ");
                throw;
            }
        }
    }
}