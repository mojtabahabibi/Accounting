using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.TimeZoneInfo;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class AccountTransactionRepository : BaseRepository<AccountTransaction>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(AccountingDbContext dbContext, ILogger<BaseRepository<AccountTransaction>> logger) : base(dbContext, logger)
        {

        }
        public async Task<List<AccountTransactionListDto>> GetAllAccountTransactionAsync()
        {
            logger.LogInformation("AccountTransactionRepository GetAllAsync was called for ");
            try
            {
                var list = new List<AccountTransactionListDto>();
                var accountTransactions = await dbContext.AccountTransactions.Include(i => i.Invocie).ThenInclude(i => i.AccountUser)
                    .Include(i => i.Payment).ThenInclude(i => i.AccountUser).ToListAsync();
                foreach (var tr in accountTransactions)
                {
                    string accountusername = "";
                    long price = 0;
                    if (tr.Payment != null)
                    {
                        accountusername = tr.Payment.AccountUser.UserName;
                        price = tr.Payment.Price;
                    }
                    else if (tr.Invocie != null)
                    {
                        accountusername = tr.Invocie.AccountUser.UserName;
                        price = tr.Invocie.TotalPrice;
                    }

                    var transaction = new AccountTransactionListDto()
                    {
                        TransactionType = tr.Payment != null ? "واریز به حساب" : "برداشت از حساب",
                        AccountUserName = accountusername,
                        Price = price,
                        TransactionNumber = tr.TransactionNumber,
                        Time = tr.Time,
                    };

                    list.Add(transaction);
                }
                logger.LogInformation("AccountTransactionRepository GetAllAsync was Done for ");
                return list;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTransactionRepository GetAllAsync was Failed for ");
                throw;
            }
        }
        public async Task<List<AccountTransactionListDto>> GetByUserNameAsync(string username)
        {
            logger.LogInformation("AccountTransactionRepository GetAllAsync was called for ");
            try
            {
                var list = new List<AccountTransactionListDto>();
                var accountTransactions = await dbContext.AccountTransactions.Include(i => i.Invocie).ThenInclude(i => i.AccountUser)
                    .Include(i => i.Payment).ThenInclude(i => i.AccountUser).ToListAsync();
                foreach (var tr in accountTransactions)
                {
                    string accountusername = "";
                    long price = 0;
                    if (tr.Payment != null)
                    {
                        accountusername = tr.Payment.AccountUser.UserName;
                        price = tr.Payment.Price;
                    }
                    else if (tr.Invocie != null)
                    {
                        accountusername = tr.Invocie.AccountUser.UserName;
                        price = tr.Invocie.TotalPrice;
                    }
                    if (accountusername.Equals(username))
                    {
                        var transaction = new AccountTransactionListDto()
                        {
                            TransactionType = tr.Payment != null ? "واریز به حساب" : "برداشت از حساب",
                            AccountUserName = accountusername,
                            Price = price,
                            TransactionNumber = tr.TransactionNumber,
                            Time = tr.Time,
                        };

                        list.Add(transaction);
                    }

                }
                logger.LogInformation("AccountTransactionRepository GetAllAsync was Done for ");
                return list;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTransactionRepository GetAllAsync was Failed for ");
                throw;
            }
        }
        public async Task<AccountTransactionListDto> GetByTransactionNumberAsync(Guid number)
        {
            logger.LogInformation("AccountTransactionRepository GetAllAsync was called for ");
            try
            {
                var tr = await dbContext.AccountTransactions.Include(i => i.Invocie).ThenInclude(i => i.AccountUser)
                    .Include(i => i.Payment).ThenInclude(i => i.AccountUser).FirstOrDefaultAsync(i => i.TransactionNumber.Equals(number));
                string accountusername = "";
                long price = 0;
                if (tr.Payment != null)
                {
                    accountusername = tr.Payment.AccountUser.UserName;
                    price = tr.Payment.Price;
                }
                else if (tr.Invocie != null)
                {
                    accountusername = tr.Invocie.AccountUser.UserName;
                    price = tr.Invocie.TotalPrice;
                }

                var transaction = new AccountTransactionListDto()
                {
                    TransactionType = tr.Payment != null ? "واریز به حساب" : "برداشت از حساب",
                    AccountUserName = accountusername,
                    Price = price,
                    TransactionNumber = tr.TransactionNumber,
                    Time = tr.Time,
                };

                logger.LogInformation("AccountTransactionRepository GetAllAsync was Done for ");
                return transaction;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTransactionRepository GetAllAsync was Failed for ");
                throw;
            }
        }
    }
}