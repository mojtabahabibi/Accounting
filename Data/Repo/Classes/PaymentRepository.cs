using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
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

            var account = await dbContext.Accounts.Include(i=>i.AccountUser).FirstOrDefaultAsync(i => i.AccountUserId == payment.Account.AccountUserId);
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
            return false;
        }
    }
}
