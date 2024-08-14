using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class TransactionsRepository : BaseRepository<Transactions>, ITransactionsRepository
    {
        public TransactionsRepository(AccountingDbContext dbContext, ILogger<BaseRepository<Transactions>> logger) : base(dbContext, logger)
        {

        }
        public async Task<List<TransactionsListDto>> GetAllTransactionsAsync()
        {
            logger.LogInformation("TransactionsRepository GetAllAsync was called for ");
            try
            {
                return await dbContext.Transactionss.Include(i => i.Invoice).ThenInclude(i => i.User).ThenInclude(i => i.Accounts)
                                                          .Include(i => i.Payment).ThenInclude(i => i.Account).ThenInclude(i => i.User)
                                                          .Include(i => i.TransactionType)
                                                          .Select(i => new TransactionsListDto()
                                                          {
                                                              AccountId = i.Payment != null ? i.Payment.AccountId : i.Invoice.User.Accounts.FirstOrDefault().Id,
                                                              TransactionId = i.Id,
                                                              RefrenceId = i.RefrenceId,
                                                              TransactionType = i.TransactionType != null ? i.TransactionType.Title : "",
                                                              AccountNumber = i.AccountNumber,
                                                              UserName = i.Payment != null ? i.Payment.Account.User.UserName : i.Invoice.User.UserName,
                                                              Price = i.Price,
                                                              TrackingNumber = i.Payment != null ? i.TrackingNumber : "",
                                                              InvoiceNumber = i.Invoice != null ? i.InvoiceNumber : "",
                                                              TransactionNumber = i.TransactionNumber,
                                                              Time = i.CreatedDate,
                                                              Description = i.Description
                                                          }).ToListAsync();
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "TransactionsRepository GetAllAsync was Failed for ");
                throw;
            }
        }
        public async Task<List<TransactionsListDto>> GetByAccountIdTransactionAsync(long accountid)
        {
            logger.LogInformation("TransactionsRepository GetAllAsync was called for ");
            try
            {
                var result = await dbContext.Transactionss.Include(i => i.Invoice).ThenInclude(i => i.User).ThenInclude(i => i.Accounts)
                                                                .Include(i => i.Payment).ThenInclude(i => i.Account).ThenInclude(i => i.User)
                                                                .Include(i => i.TransactionType)
                                                                .Select(i => new TransactionsListDto()
                                                                {
                                                                    AccountId = i.Payment != null ? i.PaymentId : i.Invoice.User.Accounts.FirstOrDefault().Id,
                                                                    TransactionId = i.Id,
                                                                    RefrenceId = i.RefrenceId,
                                                                    TransactionType = i.TransactionType != null ? i.TransactionType.Title : "",
                                                                    AccountNumber = i.AccountNumber,
                                                                    UserName = i.Payment != null ? i.Payment.Account.User.UserName : i.Invoice.User.UserName,
                                                                    Price = i.Price,
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
                logger.LogError(ex, "TransactionsRepository GetAllAsync was Failed for ");
                throw;
            }
        }
        public async Task<List<TransactionsListDto>> GetByUserNameAsync(string username)
        {
            logger.LogInformation("TransactionsRepository GetAllAsync was called for ");
            try
            {
                var result = await dbContext.Transactionss.Include(i => i.Invoice).ThenInclude(i => i.User).ThenInclude(i => i.Accounts)
                                                                .Include(i => i.Payment).ThenInclude(i => i.Account).ThenInclude(i => i.User)
                                                                .Include(i => i.TransactionType)
                                                                .Select(i => new TransactionsListDto()
                                                                {
                                                                    AccountId = i.Payment != null ? i.PaymentId : i.Invoice.User.Accounts.FirstOrDefault().Id,
                                                                    TransactionId = i.Id,
                                                                    RefrenceId = i.RefrenceId,
                                                                    TransactionType = i.TransactionType != null ? i.TransactionType.Title : "",
                                                                    AccountNumber = i.AccountNumber,
                                                                    UserName = i.Payment != null ? i.Payment.Account.User.UserName : i.Invoice.User.UserName,
                                                                    Price = i.Price,
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
                logger.LogError(ex, "TransactionsRepository GetAllAsync was Failed for ");
                throw;
            }
        }
        public async Task<List<TransactionsListDto>> GetByTransactionNumberAsync(string number)
        {
            logger.LogInformation("TransactionsRepository GetAllAsync was called for ");
            try
            {
                return await dbContext.Transactionss.Include(i => i.Invoice).ThenInclude(i => i.User).ThenInclude(i => i.Accounts)
                                                          .Include(i => i.Payment).ThenInclude(i => i.Account).ThenInclude(i => i.User)
                                                          .Include(i => i.TransactionType)
                                                          .Where(i => i.TransactionNumber.Equals(number))
                                                          .Select(i => new TransactionsListDto()
                                                          {
                                                              AccountId = i.Payment != null ? i.PaymentId : i.Invoice.User.Accounts.FirstOrDefault().Id,
                                                              TransactionId = i.Id,
                                                              RefrenceId = i.RefrenceId,
                                                              TransactionType = i.TransactionType != null ? i.TransactionType.Title : "",
                                                              AccountNumber = i.AccountNumber,
                                                              UserName = i.Payment != null ? i.Payment.Account.User.UserName : i.Invoice.User.UserName,
                                                              Price = i.Price,
                                                              TrackingNumber = i.Payment != null ? i.Payment.Id.ToString() : "",
                                                              InvoiceNumber = i.Invoice != null ? i.InvoiceNumber : "",
                                                              TransactionNumber = i.TransactionNumber,
                                                              Time = i.CreatedDate,
                                                              Description = i.Description
                                                          }).ToListAsync();
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "TransactionsRepository GetAllAsync was Failed for ");
                throw;
            }
        }
        public async Task UpdateRefrenceId(Transactions transaction1, Transactions transaction2)
        {
            logger.LogInformation("TransactionsRepository UpdateRefrenceId was called");
            try
            {
                await dbContext.SaveChangesAsync();
                transaction1.RefrenceId = transaction2.Id;
                transaction2.RefrenceId = transaction1.Id;
                dbContext.Attach(transaction1);
                dbContext.Attach(transaction2);
                logger.LogInformation("TransactionsRepository UpdateRefrenceId was Done");
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "TransactionsRepository UpdateRefrenceId was Failed");
                throw;
            }
        }
    }
}