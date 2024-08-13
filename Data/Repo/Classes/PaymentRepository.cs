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
        public PaymentRepository(AccountingDbContext dbContext, ILogger<BaseRepository<Payment>> logger) : base(dbContext, logger)
        {
        }
        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            Random random = new Random();
            await dbContext.Payments.AddAsync(payment);
            await dbContext.SaveChangesAsync();

            var transaction = new AccountTransaction()
            {
                Payment = payment,
                TransactionNumber = random.Next(99999, 1000000).ToString(),
            };
            await dbContext.AccountTransactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();

            var account = await dbContext.Accounts.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.AccountUserId == payment.Account.AccountUserId);
            if (account != null)
            {
                //var exist = await dbContext.Wallets.Include(i => i.Account).FirstOrDefaultAsync(i => i.Account.Id == account.Id);
                //if (exist == null)
                //{
                //    var wallet = new Wallet()
                //    {
                //        Account = account,
                //        Amount = payment.Price,
                //        WalletNumber = Guid.NewGuid()
                //    };
                //    await dbContext.Wallets.AddAsync(wallet);
                //}
                //else
                //{
                //    long amount = exist.Amount;
                //    exist.Amount = payment.Price + amount;
                //}
                await dbContext.SaveChangesAsync();
            }
            return payment;
        }
        public async Task<bool> DepositAsync(Payment payment)
        {
            Random random = new Random();
            var account = await dbContext.Accounts.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.Id.Equals(payment.AccountId));
            if (account != null)
            {
                if (account.AccountTypeId == 1)
                {
                    payment.InvoicePayTypeId = 1;
                    await dbContext.Payments.AddAsync(payment);
                    await dbContext.SaveChangesAsync();

                    var accountCompany = await dbContext.Accounts.Include(i => i.AccountUser).FirstOrDefaultAsync();
                    if (accountCompany != null)
                    {
                        string trackingnumber = random.Next(99999, 1000000).ToString();
                        long price = payment.Price;

                        var cashTransaction = new AccountTransaction()
                        {
                            Payment = payment,
                            TransactionTypeId = 1,
                            AccountNumber = account.AccountNumber,
                            AccountUsername = account.AccountUser?.UserName,
                            TrackingNumber = trackingnumber,
                            TransactionNumber = random.Next(99999, 1000000).ToString(),
                            Description = "ورود " + price + " وجه نقد به حساب نقدی " + account.AccountNumber + "  با شماره پیگیری  " + trackingnumber
                        };
                        var companyTransaction = new AccountTransaction()
                        {
                            Payment = payment,
                            TransactionTypeId = 1,
                            AccountNumber = accountCompany?.AccountNumber,
                            AccountUsername = accountCompany?.AccountUser?.UserName,
                            TrackingNumber = trackingnumber,
                            TransactionNumber = random.Next(99999, 1000000).ToString(),
                            Description = "ورود " + price + " وجه نقد به حساب صندوق " + accountCompany?.AccountNumber + "  با شماره پیگیری  " + trackingnumber
                        };
                        await dbContext.AccountTransactions.AddAsync(cashTransaction);
                        await dbContext.AccountTransactions.AddAsync(companyTransaction);

                        account.Amount += price;
                        accountCompany.Amount -= price;
                        dbContext.Attach(account);
                        dbContext.Attach(accountCompany);

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
            var account = await dbContext.Accounts.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.Id.Equals(model.AccountId));
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
                    invoice.Status = InvoiceStatus.Close;
                    dbContext.Attach(invoice);
                    await dbContext.SaveChangesAsync();
                }
                if (invoice.Status == InvoiceStatus.Close)
                {
                    long invoicePrice = invoice.TotalPrice;
                    if (account.Amount < invoicePrice)
                    {
                        return PaymentResult.DontMoney;
                    }
                    else
                    {
                        account.Amount -= invoicePrice;
                        dbContext.Attach(account);

                        var accountCompany = await dbContext.Accounts.Include(i => i.AccountUser).FirstOrDefaultAsync();
                        if (accountCompany != null)
                        {
                            accountCompany.Amount += invoicePrice;
                            dbContext.Attach(accountCompany);

                            var customerTransaction = new AccountTransaction()
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                TransactionTypeId = 4,
                                AccountNumber = account.AccountNumber,
                                AccountUsername = account.AccountUser?.UserName,
                                InvoiceNumber = invoice.SerialNumber,
                                TransactionNumber = random.Next(99999, 1000000).ToString(),
                                Description = "خروج " + invoicePrice + " وجه نقد از حساب کیف پول " + account.AccountNumber + "  برای پرداخت فاکتور  " + invoice.SerialNumber
                            };
                            var companyTransaction = new AccountTransaction()
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                TransactionTypeId = 5,
                                AccountNumber = accountCompany?.AccountNumber,
                                AccountUsername = accountCompany?.AccountUser?.UserName,
                                InvoiceNumber = invoice.SerialNumber,
                                TransactionNumber = random.Next(99999, 1000000).ToString(),
                                Description = "ورود " + invoicePrice + " وجه نقد به حساب صندوق " + accountCompany?.AccountNumber + "  برای پرداخت فاکتور  " + invoice.SerialNumber
                            };
                            await dbContext.AccountTransactions.AddAsync(customerTransaction);
                            await dbContext.AccountTransactions.AddAsync(companyTransaction);

                            invoice.Status = InvoiceStatus.Pay;
                            dbContext.Attach(invoice);

                            await dbContext.SaveChangesAsync();

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
