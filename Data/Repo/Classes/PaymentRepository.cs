using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly ITransactionsRepository transactionRepository;
        public PaymentRepository(AccountingDbContext dbContext, ILogger<BaseRepository<Payment>> logger
            , IInvoiceRepository invoiceRepository, ITransactionsRepository transactionRepository) : base(dbContext, logger)
        {
            this.invoiceRepository = invoiceRepository;
            this.transactionRepository = transactionRepository;
        }
        public async Task<bool> DepositAsync(Payment payment)
        {
            Random random = new Random();
            var account = await dbContext.Accounts.Include(i => i.User).FirstOrDefaultAsync(i => i.Id.Equals(payment.AccountId));
            if (account != null)
            {
                if (account.AccountTypeId == 1)
                {
                    payment.InvoicePayTypeId = 1;
                    await dbContext.Payments.AddAsync(payment);
                    await dbContext.SaveChangesAsync();

                    var accountCompany = await dbContext.Accounts.Include(i => i.User).FirstOrDefaultAsync();
                    if (accountCompany != null)
                    {
                        string trackingnumber = random.Next(99999, 1000000).ToString();
                        long price = payment.Price;

                        account.Amount += price;
                        accountCompany.Amount -= price;
                        dbContext.Attach(account);
                        dbContext.Attach(accountCompany);

                        var cashTransaction = new Transactions()
                        {
                            Payment = payment,
                            TransactionTypeId = 1,
                            AccountNumber = account.AccountNumber,
                            Username = account.User?.UserName,
                            TrackingNumber = trackingnumber,
                            TransactionNumber = random.Next(99999, 1000000).ToString(),
                            Price = price,
                            Description = "ورود " + price + " وجه نقد به حساب نقدی " + account.AccountNumber + "  با شماره پیگیری  " + trackingnumber
                        };
                        var companyTransaction = new Transactions()
                        {
                            Payment = payment,
                            TransactionTypeId = 1,
                            AccountNumber = accountCompany?.AccountNumber,
                            Username = accountCompany?.User?.UserName,
                            TrackingNumber = trackingnumber,
                            TransactionNumber = random.Next(99999, 1000000).ToString(),
                            Price = price,
                            Description = "ورود " + price + " وجه نقد به حساب صندوق " + accountCompany?.AccountNumber + "  با شماره پیگیری  " + trackingnumber
                        };
                        await dbContext.Transactionss.AddAsync(cashTransaction);
                        await dbContext.Transactionss.AddAsync(companyTransaction);

                        await transactionRepository.UpdateRefrenceId(cashTransaction, companyTransaction);

                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }
        public async Task<PaymentResult> PaymentAsync(PaymentInvoiceDto model)
        {
            Random random = new Random();
            var account = await dbContext.Accounts.Include(i => i.User).FirstOrDefaultAsync(i => i.Id.Equals(model.AccountId));
            if (account?.AccountTypeId == 1)
            {
                return PaymentResult.PayWithWallet;
            }
            else if (account?.AccountTypeId == 2)
            {
                var invoice = await dbContext.Invoices.FirstOrDefaultAsync(i => i.Id.Equals(model.InvoiceId) && i.DeletedDate == null);
                if (invoice == null)
                {
                    return PaymentResult.DontInvoice;
                }
                if (invoice.Status == InvoiceStatus.Open)
                {
                    var invoiceX = new InvoiceX
                    {
                        InvoiceId = invoice.Id,
                        Invoice = invoice,
                        Status = InvoiceStatus.Close
                    };
                    await invoiceRepository.ChangeStatus(invoiceX);
                }
                if (invoice.Status == InvoiceStatus.Close || invoice.Status == InvoiceStatus.Pending)
                {
                    long invoicePrice = invoice.TotalPrice;
                    long price = model.Price;
                    if (price > invoicePrice)
                    {
                        return PaymentResult.MostPrice;
                    }
                    if (account.Amount < price)
                    {
                        return PaymentResult.DontMoney;
                    }
                    else
                    {
                        long p = 0;
                        var tr = dbContext.Transactionss.Where(i => i.InvoiceId.Equals(invoice.Id));
                        if (tr != null)
                        {
                            long payment = tr.Sum(i => i.Price) / 2;
                            p = invoicePrice - payment;
                            if (price > p)
                                return PaymentResult.MostPrice;
                        }
                        account.Amount -= price;
                        dbContext.Attach(account);

                        var accountCompany = await dbContext.Accounts.Include(i => i.User).FirstOrDefaultAsync();
                        if (accountCompany != null)
                        {
                            accountCompany.Amount += price;
                            dbContext.Attach(accountCompany);

                            var customerTransaction = new Transactions()
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                TransactionTypeId = 4,
                                AccountNumber = account.AccountNumber,
                                Username = account.User?.UserName,
                                InvoiceNumber = invoice.SerialNumber,
                                TransactionNumber = random.Next(99999, 1000000).ToString(),
                                Price = price,
                                Description = "خروج " + price + " وجه نقد از حساب کیف پول " + account.AccountNumber + "  برای پرداخت فاکتور  " + invoice.SerialNumber
                            };
                            var companyTransaction = new Transactions()
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                TransactionTypeId = 5,
                                AccountNumber = accountCompany?.AccountNumber,
                                Username = accountCompany?.User?.UserName,
                                InvoiceNumber = invoice.SerialNumber,
                                TransactionNumber = random.Next(99999, 1000000).ToString(),
                                Price = price,
                                Description = "ورود " + price + " وجه نقد به حساب صندوق " + accountCompany?.AccountNumber + "  برای پرداخت فاکتور  " + invoice.SerialNumber
                            };
                            await dbContext.Transactionss.AddAsync(customerTransaction);
                            await dbContext.Transactionss.AddAsync(companyTransaction);

                            await transactionRepository.UpdateRefrenceId(customerTransaction, companyTransaction);

                            InvoiceStatus status = (p == 0 && price == invoicePrice) ? InvoiceStatus.Pay : (p == 0 && price < invoicePrice) ? InvoiceStatus.Pending :
                                                   (p != 0 && price == p) ? InvoiceStatus.Pay : InvoiceStatus.Pending;
                            var invoiceX = new InvoiceX
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                Status = status
                            };
                            await invoiceRepository.ChangeStatus(invoiceX);

                            return PaymentResult.Done;
                        }
                        return PaymentResult.InnerExeption;
                    }
                }
                return PaymentResult.StatusInvoice;
            }
            return PaymentResult.DontAccount;
        }
    }
}