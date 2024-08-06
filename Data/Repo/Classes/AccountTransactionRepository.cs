using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                var accountTransactions = await dbContext.AccountTransactions.Include(i => i.Invoice).ThenInclude(i => i.AccountUser)
                    .Include(i => i.Payment).ThenInclude(i => i.Account).Include(i => i.TransactionType).ToListAsync();
                foreach (var tr in accountTransactions)
                {
                    string accountusername = "";
                    long price = 0;
                    if (tr.Payment != null)
                    {
                        if (tr.Payment.Account.AccountUser != null)
                        {
                            accountusername = tr.Payment.Account.AccountUser.UserName;
                            price = tr.Payment.Price;
                        }
                    }
                    else if (tr.Invoice != null)
                    {
                        if (tr.Invoice.AccountUser != null)
                        {
                            accountusername = tr.Invoice.AccountUser.UserName;
                            price = tr.Invoice.TotalPrice;
                        }
                    }

                    var transaction = new AccountTransactionListDto()
                    {
                        TransactionType = tr.TransactionType.Title,
                        AccountUserName = accountusername,
                        Price = price,
                        TransactionNumber = tr.TransactionNumber,
                        Time = tr.CreatedDate,
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
                var accountTransactions = await dbContext.AccountTransactions.Include(i => i.Invoice).ThenInclude(i => i.AccountUser)
                    .Include(i => i.Payment).ThenInclude(i => i.Account).Include(i=>i.TransactionType).ToListAsync();
                foreach (var tr in accountTransactions)
                {
                    string accountusername = "";
                    long price = 0;
                    if (tr.Payment != null)
                    {
                        if (tr.Payment.Account.AccountUser != null)
                        {
                            accountusername = tr.Payment.Account.AccountUser.UserName;
                            price = tr.Payment.Price;
                        }
                    }
                    else if (tr.Invoice != null)
                    {
                        if (tr.Invoice.AccountUser != null)
                        {
                            accountusername = tr.Invoice.AccountUser.UserName;
                            price = tr.Invoice.TotalPrice;
                        }
                    }

                    if (accountusername.Equals(username))
                    {
                        var transaction = new AccountTransactionListDto()
                        {
                            TransactionType = tr.TransactionType.Title,
                            AccountUserName = accountusername,
                            Price = price,
                            TransactionNumber = tr.TransactionNumber,
                            Time = tr.CreatedDate,
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
                var tr = await dbContext.AccountTransactions.Include(i => i.Invoice).ThenInclude(i=>i.AccountUser)
                    .Include(i => i.Payment).ThenInclude(i => i.Account).Include(i=>i.TransactionType).FirstOrDefaultAsync(i => i.TransactionNumber.Equals(number));
                string accountusername = "";
                long price = 0;
                if (tr.Payment != null)
                {
                    if (tr.Payment.Account.AccountUser != null)
                    {
                        accountusername = tr.Payment.Account.AccountUser.UserName;
                        price = tr.Payment.Price;
                    }
                }
                else if (tr.Invoice != null)
                {
                    if (tr.Invoice.AccountUser != null)
                    {
                        accountusername = tr.Invoice.AccountUser.UserName;
                        price = tr.Invoice.TotalPrice;
                    }
                }

                var transaction = new AccountTransactionListDto()
                {
                    TransactionType = tr.TransactionType.Title,
                    AccountUserName = accountusername,
                    Price = price,
                    TransactionNumber = tr.TransactionNumber,
                    Time = tr.CreatedDate,
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