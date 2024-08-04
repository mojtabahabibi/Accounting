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

            var account = await dbContext.Accounts.FirstOrDefaultAsync(i => i.AccountUserId == payment.AccountUserId);
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
    }
}
