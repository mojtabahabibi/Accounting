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
        private readonly ITransactionsRepository transactionRepository;
        public InvoiceRepository(AccountingDbContext dbContext, ILogger<BaseRepository<Invoice>> logger, ITransactionsRepository transactionRepository) : base(dbContext, logger)
        {
            this.transactionRepository = transactionRepository;
        }
        public async Task<List<InvoiceListDto>> GetAllInvoiceAsync()
        {
            logger.LogInformation("InvoiceRepository GetAllAsync was called for ");
            try
            {
                var entity = await dbContext.Invoices.Include(i => i.InvoiceItems).ThenInclude(i => i.Item).Where(i => i.DeletedDate == null)
                    .Select(i => new InvoiceListDto
                    {
                        UserId = i.UserId,
                        InvoiceId = i.Id,
                        Title = i.Title,
                        SerialNumber = i.SerialNumber,
                        Price = i.Price,
                        Off = i.Off,
                        TotalPrice = i.TotalPrice,
                        Status = i.Status == InvoiceStatus.Open ? "فاکتور باز است" : i.Status == InvoiceStatus.Close ? "فاکتور بسته شده است" :
                                 i.Status == InvoiceStatus.Cancel ? "فاکتور کنسل شده است" : i.Status == InvoiceStatus.Pay ? "فاکتور پرداخت شده است" :
                                 i.Status == InvoiceStatus.Delete ? "فاکتور حذف شده است" : i.Status == InvoiceStatus.Pending ? "فاکتور درحال پرداخت است" : "فاکتور لغو شده است",
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
                        UserId = i.UserId,
                        InvoiceId = i.Id,
                        Title = i.Title,
                        SerialNumber = i.SerialNumber,
                        Price = i.Price,
                        Off = i.Off,
                        TotalPrice = i.TotalPrice,
                        Status = i.Status == InvoiceStatus.Open ? "فاکتور باز است" : i.Status == InvoiceStatus.Close ? "فاکتور بسته شده است" :
                                 i.Status == InvoiceStatus.Cancel ? "فاکتور کنسل شده است" : i.Status == InvoiceStatus.Pay ? "فاکتور پرداخت شده است" :
                                 i.Status == InvoiceStatus.Delete ? "فاکتور حذف شده است" : i.Status == InvoiceStatus.Pending ? "فاکتور درحال پرداخت است" : "فاکتور لغو شده است",
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
        public async Task<bool> DeleteInvoiceAsync(long invoiceId)
        {
            logger.LogInformation("InvoiceRepository DeleteAsync was called");
            try
            {
                var entity = dbContext.Invoices.Find(invoiceId);
                if (entity == null)
                    throw new AccountingException("Not found ID", false, ErrorCodes.NotFound);
                entity.DeletedDate = DateTime.Now;
                var invoiceX = new InvoiceX
                {
                    InvoiceId = invoiceId,
                    Status = InvoiceStatus.Delete
                };
                await ChangeStatus(invoiceX);
                logger.LogInformation("InvoiceRepository DeleteAsync was Done for");
                return true;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository DeleteAsync was Failed for");
                throw;
            }
        }
        public async Task<bool> CloseInvoiceAsync(long invoiceId)
        {
            logger.LogInformation("InvoiceRepository CloseInvoice was called");
            try
            {
                var invoice = await dbContext.Invoices.FindAsync(invoiceId);
                if (invoice != null && invoice.Status == InvoiceStatus.Open)
                {
                    var invoiceX = new InvoiceX
                    {
                        InvoiceId = invoiceId,
                        Invoice = invoice,
                        Status = InvoiceStatus.Close
                    };
                    await ChangeStatus(invoiceX);
                    logger.LogInformation("InvoiceRepository CloseInvoice was Done");
                    return true;
                }
                else
                    return false;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository CloseInvoice was Failed");
                throw;
            }
        }
        public async Task<bool> ReturnedInvoiceAsync(long invoiceId)
        {
            logger.LogInformation("InvoiceRepository ReturnedInvoice was called");
            try
            {
                Random random = new Random();
                var invoice = await dbContext.Invoices.Include(i => i.User).FirstOrDefaultAsync(i => i.Id.Equals(invoiceId));
                if (invoice != null)
                {
                    if (invoice.Status == InvoiceStatus.Pay)
                    {
                        long price = invoice.TotalPrice;
                        long UserId = invoice.UserId;

                        var accountWallet = await dbContext.Accounts.Include(i => i.User).FirstOrDefaultAsync(i => i.UserId.Equals(UserId) && i.AccountTypeId == 2);
                        var accountCompany = await dbContext.Accounts.FirstAsync();

                        if (accountWallet != null && accountCompany != null)
                        {
                            accountWallet.Amount += price;
                            accountCompany.Amount -= price;

                            dbContext.Attach(accountWallet);
                            dbContext.Attach(accountCompany);

                            var walletTransaction = new Transactions()
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                TransactionTypeId = 7,
                                AccountNumber = accountWallet.AccountNumber,
                                Username = accountWallet.User?.UserName,
                                InvoiceNumber = invoice.SerialNumber,
                                TransactionNumber = random.Next(99999, 1000000).ToString(),
                                Price = price,
                                Description = "ورود " + price.ToString() + " وجه نقد به حساب کیف پول " + accountWallet.AccountNumber + " بابت مرجوع فاکتور  " + invoice.SerialNumber
                            };
                            var companyTransaction = new Transactions()
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                TransactionTypeId = 7,
                                AccountNumber = accountCompany?.AccountNumber,
                                Username = accountCompany?.User?.UserName,
                                InvoiceNumber = invoice.SerialNumber,
                                TransactionNumber = random.Next(99999, 1000000).ToString(),
                                Price = price,
                                Description = "خروج " + price.ToString() + " وجه نقد از حساب صندوق  " + accountCompany?.AccountNumber + " بابت مرجوع فاکتور " + invoice.SerialNumber
                            };
                            await dbContext.Transactionss.AddAsync(walletTransaction);
                            await dbContext.Transactionss.AddAsync(companyTransaction);

                            await transactionRepository.UpdateRefrenceId(walletTransaction, companyTransaction);

                            var invoiceX = new InvoiceX
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                Status = InvoiceStatus.Returen
                            };
                            await ChangeStatus(invoiceX);

                            logger.LogInformation("InvoiceRepository ReturnedInvoice was Done");
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository ReturnedInvoice was Failed");
                throw;
            }
        }
        public async Task<bool> BuyChargeAsync(BuyChargeDto model)
        {
            logger.LogInformation("InvoiceRepository BuyCharge was called");
            try
            {
                Random random = new Random();
                string serialNumber = random.Next(99999, 1000000).ToString();

                var invoice = new Invoice()
                {
                    UserId = model.UserId,
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

                    var invoiceX = new InvoiceX
                    {
                        InvoiceId = invoice.Id,
                        Invoice = invoice,
                        Status = InvoiceStatus.Open
                    };
                    await ChangeStatus(invoiceX);

                    logger.LogInformation("InvoiceRepository BuyCharge was Done");
                    return true;
                }
                return false;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository BuyCharge was Failed");
                throw;
            }
        }
        public async Task<CancelInvoiceResult> CancelInvoiceAsync(long invoiceId)
        {
            logger.LogInformation("InvoiceRepository CancelInvoice was called");
            try
            {
                var invoice = await dbContext.Invoices.FindAsync(invoiceId);
                if (invoice != null)
                {
                    switch (invoice.Status)
                    {
                        case InvoiceStatus.Open:
                            var invoiceX = new InvoiceX
                            {
                                InvoiceId = invoiceId,
                                Invoice = invoice,
                                Status = InvoiceStatus.Cancel
                            };
                            await ChangeStatus(invoiceX);
                            logger.LogInformation("InvoiceRepository CancelInvoice was Done");
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
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository CancelInvoice was Failed");
                throw;
            }
        }
        public async Task<PaymentResult> PaymentChargeAsync(PaymentChargeDto model)
        {
            logger.LogInformation("InvoiceRepository PaymentCharge was called");
            try
            {
                Random random = new Random();
                var account = await dbContext.Accounts.Include(i => i.User).Where(i => i.UserId.Equals(model.UserId)).ToListAsync();
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
                            var invoiceX = new InvoiceX
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                Status = InvoiceStatus.Close
                            };
                            await ChangeStatus(invoiceX);
                        }
                        if (invoice.Status == InvoiceStatus.Close)
                        {
                            var accountCash = account.First(i => i.AccountTypeId == 1);
                            var accountWallet = account.First(i => i.AccountTypeId == 2);
                            long price = invoice.TotalPrice;

                            if (accountCash.Amount > price)
                            {
                                accountCash.Amount -= price;
                                accountWallet.Amount += price;
                                dbContext.AttachRange(account);

                                var cashTransaction = new Transactions()
                                {
                                    InvoiceId = invoice.Id,
                                    Invoice = invoice,
                                    TransactionTypeId = 2,
                                    AccountNumber = accountCash.AccountNumber,
                                    Username = accountCash.User?.UserName,
                                    InvoiceNumber = invoice.SerialNumber,
                                    TransactionNumber = random.Next(99999, 1000000).ToString(),
                                    Price = price,
                                    Description = "خروج " + price.ToString() + " وجه نقد از حساب نقدی " + accountCash.AccountNumber + " به حساب کیف پول " + accountWallet?.AccountNumber
                                };
                                var walletTransaction = new Transactions()
                                {
                                    InvoiceId = invoice.Id,
                                    Invoice = invoice,
                                    TransactionTypeId = 3,
                                    AccountNumber = accountWallet?.AccountNumber,
                                    Username = accountWallet?.User?.UserName,
                                    InvoiceNumber = invoice.SerialNumber,
                                    TransactionNumber = random.Next(99999, 1000000).ToString(),
                                    Price = price,
                                    Description = "ورود " + price.ToString() + " وجه نقد به حساب کیف پول " + accountWallet?.AccountNumber + " از حساب نقدی " + accountCash?.AccountNumber
                                };
                                await dbContext.Transactionss.AddAsync(cashTransaction);
                                await dbContext.Transactionss.AddAsync(walletTransaction);

                                await transactionRepository.UpdateRefrenceId(cashTransaction, walletTransaction);

                                var invoiceX = new InvoiceX()
                                {
                                    InvoiceId = invoice.Id,
                                    Invoice = invoice,
                                    Status = InvoiceStatus.Pay
                                };
                                await ChangeStatus(invoiceX);

                                logger.LogInformation("InvoiceRepository PaymentCharge was Done");
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
                logger.LogError(ex, "InvoiceRepository PaymentCharge was Failed");
                throw;
            }
        }
        public async Task<InvoiceStatusListDto> InvoiceStatusListAsync(long invoiceId)
        {
            logger.LogInformation("InvoiceRepository InvoiceStatusList was called");
            try
            {
                var query = dbContext.InvoiceXes.Include(i => i.Invoice).Where(i => i.InvoiceId.Equals(invoiceId));
                if (query != null)
                {
                    var invoice = await query.FirstAsync();
                    var statuslist = query.Select(i => new InvoiceStatusDto
                    {
                        ChangeTime = i.CreatedDate,
                        Status = i.Status == InvoiceStatus.Open ? "فاکتور باز است" : i.Status == InvoiceStatus.Close ? "فاکتور بسته شده است" :
                                 i.Status == InvoiceStatus.Cancel ? "فاکتور کنسل شده است" : i.Status == InvoiceStatus.Pay ? "فاکتور پرداخت شده است" :
                                 i.Status == InvoiceStatus.Delete ? "فاکتور حذف شده است" : i.Status == InvoiceStatus.Pending ? "فاکتور درحال پرداخت است" : "فاکتور لغو شده است",
                    }).ToList();

                    var result = new InvoiceStatusListDto()
                    {
                        InvoiceId = invoiceId,
                        Title = invoice.Invoice?.Title,
                        SerialNumber = invoice.Invoice?.SerialNumber,
                        TotalPrice = invoice.Invoice?.TotalPrice,
                        InvoiceStatusDtos = statuslist
                    };
                    logger.LogInformation("InvoiceRepository InvoiceStatusList was Done");
                    return result;
                }
                return new InvoiceStatusListDto()
                {
                    InvoiceStatusDtos = new List<InvoiceStatusDto>()
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceRepository InvoiceStatusList was Failed");
                throw;
            }
        }
        public async Task ChangeStatus(InvoiceX model)
        {
            logger.LogInformation("InvoiceXRepository ChangeStatus was called");
            try
            {
                var invoice = await dbContext.Invoices.FindAsync(model.InvoiceId);
                if (invoice != null)
                {
                    invoice.Status = model.Status;
                    dbContext.Attach(invoice);
                    await dbContext.AddAsync(model);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("InvoiceXRepository ChangeStatus was Done");
                }
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceXRepository ChangeStatus was Failed");
                throw;
            }
        }
    }
}