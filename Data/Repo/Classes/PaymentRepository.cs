﻿using EcoBar.Accounting.Data.Configs;
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
            await dbContext.Payments.AddAsync(payment);
            await dbContext.SaveChangesAsync();

            var transaction = new AccountTransaction()
            {
                PaymentId = payment.Id,
                Payment = payment,
                TransactionNumber = Guid.NewGuid(),
                Time = DateTime.Now
            };
            await dbContext.AccountTransactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();

            var account = await dbContext.Accounts.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.AccountUserId == payment.Account.AccountUserId);
            if (account != null)
            {
                var exist = await dbContext.Wallets.FirstOrDefaultAsync(i => i.AccountId == account.Id);
                if (exist == null)
                {
                    var wallet = new Wallet()
                    {
                        Account = account,
                        AccountId = account.Id,
                        Amount = payment.Price,
                        WalletNumber = Guid.NewGuid()
                    };
                    await dbContext.Wallets.AddAsync(wallet);
                }
                else
                {
                    long amount = exist.Amount;
                    exist.Amount = payment.Price + amount;
                }
                await dbContext.SaveChangesAsync();
            }
            return payment;
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
                    if (type != null)
                    {
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

                        var account = await dbContext.Accounts.FindAsync(payment.AccountId);
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
                            account.Amount += payment.Price;
                        }

                        var accountCompany = await dbContext.Accounts.FirstOrDefaultAsync(i => i.AccountTypeId == 1);
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
                            accountCompany.Amount -= payment.Price;
                        }

                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }
        public async Task<TransferResult> TransferAsync(TransferDto model)
        {
            var accountCash = await dbContext.Accounts.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.Id.Equals(model.AccountCashId) && i.AccountTypeId == 1);
            var accountWallet = await dbContext.Accounts.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.Id.Equals(model.AccountWalletId) && i.AccountTypeId == 2);
            if (accountWallet != null)
            {
                if (accountCash != null)
                {
                    if (accountCash.Amount > model.Price)
                    {
                        var item = await dbContext.Items.FirstOrDefaultAsync();
                        if (item != null)
                        {
                            var invoice = new Invoice()
                            {
                                AccountUserId = accountCash.AccountUserId,
                                AccountUser = accountCash.AccountUser,
                                Title = "خرید شارژ",
                                SerialNumber = Guid.NewGuid().ToString(),
                                Price = model.Price,
                                Off = 0,
                                TotalPrice = model.Price,
                                Date = DateTime.Now,
                                Status = true,
                            };
                            await dbContext.Invoices.AddAsync(invoice);
                            await dbContext.SaveChangesAsync();

                            var invoiceItem = new InvoiceItem()
                            {
                                Item = item,
                                ItemId = item.Id,
                                Invoice = invoice,
                                InvoiceId = invoice.Id,
                                Name = "خرید شارژ",
                                Count = 1,
                                Off = 0,
                                Price = model.Price
                            };
                            await dbContext.InvoiceItems.AddAsync(invoiceItem);
                            var wallet = await dbContext.Wallets.FirstOrDefaultAsync(i => i.AccountId.Equals(accountWallet.Id));
                            if (wallet != null)
                                wallet.Amount = model.Price;
                            accountCash.Amount -= model.Price;
                            accountWallet.Amount += model.Price;

                            // Create Transaction
                            var transaction = new AccountTransaction()
                            {
                                InvoiceId = invoice.Id,
                                Invoice = invoice,
                                TransactionTypeId = 3,
                                TransactionNumber = Guid.NewGuid(),
                                Time = DateTime.Now
                            };
                            await dbContext.AccountTransactions.AddAsync(transaction);
                            await dbContext.SaveChangesAsync();

                            // Create AccountBook
                            var accountBook1 = new AccountBook()
                            {
                                TransactionId = transaction.Id,
                                AccountTransaction = transaction,
                                AccountId = accountCash.Id,
                                Account = accountCash,
                                Amount = -(model.Price)
                            };
                            var accountBook2 = new AccountBook()
                            {
                                TransactionId = transaction.Id,
                                AccountTransaction = transaction,
                                AccountId = accountWallet.Id,
                                Account = accountWallet,
                                Amount = model.Price
                            };
                            await dbContext.AccountBooks.AddAsync(accountBook1);
                            await dbContext.AccountBooks.AddAsync(accountBook2);
                        }
                        await dbContext.SaveChangesAsync();

                        return TransferResult.Success;
                    }
                    return TransferResult.ErrorAccountWalletBalance;
                }
                return TransferResult.ErrorAccountCash;
            }
            return TransferResult.ErrorAccountWallet;
        }
        public async Task<PaymentResult> PaymentAsync(PaymentInvoiceDto model)
        {
            var invoice = await dbContext.Invoices.Include(i => i.AccountUser).FirstOrDefaultAsync(i => i.Id.Equals(model.InvoiceId));
            if (invoice != null)
            {
                if (invoice.AccountUser != null)
                {
                    var accountUserId = invoice.AccountUserId;
                    var account = await dbContext.Accounts.Include(i => i.AccountUser)
                        .FirstOrDefaultAsync(i => i.AccountUserId == accountUserId && i.Id == model.AccountId);
                    if (account != null)
                    {
                        if (account.AccountTypeId == 1)
                        {
                            // حساب نقدی
                            if (account.Amount > invoice.TotalPrice)
                            {
                                invoice.Status = true;
                                var transaction = new AccountTransaction()
                                {
                                    InvoiceId = model.InvoiceId,
                                    Invoice = invoice,
                                    TransactionTypeId = 2,
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

                                    account.Amount -= invoice.TotalPrice;
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

                                    accountCompany.Amount += invoice.TotalPrice;
                                }
                                await dbContext.SaveChangesAsync();
                                return PaymentResult.Done;
                            }
                            return PaymentResult.DontMoney;
                        }
                        else if (account.AccountTypeId == 2)
                        {
                            // حساب کیف پول
                            var wallets = await dbContext.Wallets.Include(i => i.Account).FirstOrDefaultAsync(i => i.Account.AccountUserId.Equals(accountUserId));
                            long totalPrice = invoice.TotalPrice;
                            if (totalPrice != 0 && wallets != null)
                            {
                                invoice.Status = true;
                                if (wallets.Amount > totalPrice)
                                {
                                    //var type = await dbContext.TransactionTypes.FirstOrDefaultAsync(i => i.Id == 2);
                                    var transaction = new AccountTransaction()
                                    {
                                        InvoiceId = model.InvoiceId,
                                        Invoice = invoice,
                                        TransactionTypeId = 4,
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

                                        accountCompany.Amount += invoice.TotalPrice;
                                    }
                                    await dbContext.SaveChangesAsync();
                                    return PaymentResult.Done;
                                }
                                return PaymentResult.DontMoney;
                            }
                        }
                    }
                }
                return PaymentResult.DontInvoiceItem;
            }
            return PaymentResult.DontInvoiceItem;
        }
    }
}
