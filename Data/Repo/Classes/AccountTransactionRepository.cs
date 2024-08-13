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
                return await dbContext.AccountTransactions.Include(i => i.Invoice).ThenInclude(i => i.AccountUser).ThenInclude(i=>i.Accounts)
                                                          .Include(i => i.Payment).ThenInclude(i => i.Account).ThenInclude(i => i.AccountUser)
                                                          .Include(i => i.TransactionType)
                                                          .Select(i => new AccountTransactionListDto()
                                                          {
                                                              AccountId = i.Payment != null ? i.PaymentId : i.Invoice.AccountUser.Accounts.FirstOrDefault().Id,
                                                              TransactionType = i.TransactionType != null ? i.TransactionType.Title : "",
                                                              AccountNumber = i.AccountNumber,
                                                              UserName = i.Payment != null ? i.Payment.Account.AccountUser.UserName : i.Invoice.AccountUser.UserName,
                                                              Price = i.Payment != null ? i.Payment.Price : i.Invoice.TotalPrice,
                                                              TrackingNumber = i.Payment != null ? i.TrackingNumber : "",
                                                              InvoiceNumber = i.Invoice != null ? i.InvoiceNumber : "",
                                                              TransactionNumber = i.TransactionNumber,
                                                              Time = i.CreatedDate,
                                                              Description = i.Description
                                                          }).ToListAsync();
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTransactionRepository GetAllAsync was Failed for ");
                throw;
            }
        }
        public async Task<List<AccountTransactionListDto>> GetByAccountIdTransactionAsync(long accountid)
        {
            logger.LogInformation("AccountTransactionRepository GetAllAsync was called for ");
            try
            {
                var result = await dbContext.AccountTransactions.Include(i => i.Invoice).ThenInclude(i => i.AccountUser).ThenInclude(i => i.Accounts)
                                                                .Include(i => i.Payment).ThenInclude(i => i.Account).ThenInclude(i => i.AccountUser)
                                                                .Include(i => i.TransactionType)
                                                                .Select(i => new AccountTransactionListDto()
                                                                {
                                                                    AccountId = i.Payment != null ? i.PaymentId : i.Invoice.AccountUser.Accounts.FirstOrDefault().Id,
                                                                    TransactionType = i.TransactionType != null ? i.TransactionType.Title : "",
                                                                    AccountNumber = i.AccountNumber,
                                                                    UserName = i.Payment != null ? i.Payment.Account.AccountUser.UserName : i.Invoice.AccountUser.UserName,
                                                                    Price = i.Payment != null ? i.Payment.Price : i.Invoice.TotalPrice,
                                                                    TrackingNumber = i.Payment != null ? i.TrackingNumber : "",
                                                                    InvoiceNumber = i.Invoice != null ? i.InvoiceNumber : "",
                                                                    TransactionNumber = i.TransactionNumber,
                                                                    Time = i.CreatedDate,
                                                                    Description = i.Description
                                                                }).ToListAsync();
                result.Where(i => i.AccountId.Equals(accountid));
                return result;
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
                var result = await dbContext.AccountTransactions.Include(i => i.Invoice).ThenInclude(i => i.AccountUser).ThenInclude(i=>i.Accounts)
                                                                .Include(i => i.Payment).ThenInclude(i => i.Account).ThenInclude(i => i.AccountUser)
                                                                .Include(i => i.TransactionType)
                                                                .Select(i => new AccountTransactionListDto()
                                                                {
                                                                    AccountId = i.Payment != null ? i.PaymentId : i.Invoice.AccountUser.Accounts.FirstOrDefault().Id,
                                                                    TransactionType = i.TransactionType != null ? i.TransactionType.Title : "",
                                                                    AccountNumber = i.AccountNumber,
                                                                    UserName = i.Payment != null ? i.Payment.Account.AccountUser.UserName : i.Invoice.AccountUser.UserName,
                                                                    Price = i.Payment != null ? i.Payment.Price : i.Invoice.TotalPrice,
                                                                    TrackingNumber = i.Payment != null ? i.TrackingNumber : "",
                                                                    InvoiceNumber = i.Invoice != null ? i.InvoiceNumber : "",
                                                                    TransactionNumber = i.TransactionNumber,
                                                                    Time = i.CreatedDate,
                                                                    Description = i.Description
                                                                }).ToListAsync();
                return result.Where(i => i.UserName.Equals(username)).ToList();
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTransactionRepository GetAllAsync was Failed for ");
                throw;
            }
        }
        public async Task<List<AccountTransactionListDto>> GetByTransactionNumberAsync(string number)
        {
            logger.LogInformation("AccountTransactionRepository GetAllAsync was called for ");
            try
            {
                return await dbContext.AccountTransactions.Include(i => i.Invoice).ThenInclude(i => i.AccountUser).ThenInclude(i=>i.Accounts)
                                                          .Include(i => i.Payment).ThenInclude(i => i.Account).ThenInclude(i => i.AccountUser)
                                                          .Include(i => i.TransactionType)
                                                          .Where(i => i.TransactionNumber.Equals(number))
                                                          .Select(i => new AccountTransactionListDto()
                                                          {
                                                              AccountId = i.Payment != null ? i.PaymentId : i.Invoice.AccountUser.Accounts.FirstOrDefault().Id,
                                                              TransactionType = i.TransactionType != null ? i.TransactionType.Title : "",
                                                              AccountNumber = i.AccountNumber,
                                                              UserName = i.Payment != null ? i.Payment.Account.AccountUser.UserName : i.Invoice.AccountUser.UserName,
                                                              Price = i.Payment != null ? i.Payment.Price : i.Invoice.TotalPrice,
                                                              TrackingNumber = i.Payment != null ? i.Payment.Id.ToString() : "",
                                                              InvoiceNumber = i.Invoice != null ? i.InvoiceNumber : "",
                                                              TransactionNumber = i.TransactionNumber,
                                                              Time = i.CreatedDate,
                                                              Description = i.Description
                                                          }).ToListAsync();
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTransactionRepository GetAllAsync was Failed for ");
                throw;
            }
        }
    }
}