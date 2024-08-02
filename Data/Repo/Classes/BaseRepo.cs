using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
    {
        protected readonly AccountingDbContext dbContext;
        protected readonly ILogger<BaseRepo<T>> logger;

        public BaseRepo(AccountingDbContext dbContext, ILogger<BaseRepo<T>> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            logger.LogInformation("BaseRepo GetAllAsync was called for " + typeof(T).FullName);
            try
            {
                var entity = await dbContext.Set<T>().Where(i => i.DeletedDate == null).ToListAsync();
                logger.LogInformation("BaseRepo GetAllAsync was Done for " + typeof(T).FullName);
                return entity;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "BaseRepo GetAllAsync was Failed for " + typeof(T).FullName);
                throw;
            }
        }
        public async Task<T> GetByIdAsync(long id)
        {
            logger.LogInformation("BaseRepo DeleteAsync was called for " + typeof(T).FullName);
            try
            {
                var entity = await dbContext.Set<T>().FirstOrDefaultAsync(i => i.Id == id && i.DeletedDate == null);
                if (entity == null) throw new AccountingException("Not found ID", false, ErrorCodes.NotFound);
                logger.LogInformation("BaseRepo DeleteAsync was Done for " + typeof(T).FullName);
                return entity;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "BaseRepo DeleteAsync was Failed for " + typeof(T).FullName);
                throw;
            }
        }
        public async Task<T> AddAsync(T entity)
        {
            logger.LogInformation("BaseRepo AddAsync was called for " + typeof(T).FullName);
            try
            {
                dbContext.Set<T>().Add(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("BaseRepo AddAsync was Done for " + typeof(T).FullName);
                return entity;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "BaseRepo AddAsync was Failed for " + typeof(T).FullName);
                throw;
            }
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            logger.LogInformation("BaseRepo UpdateAsync was called for " + typeof(T).FullName);
            try
            {
                var eentity = await dbContext.Set<T>().FindAsync(entity.Id);
                if (eentity == null)
                    throw new AccountingException("Not found ID", false, ErrorCodes.NotFound);
                else
                {
                    dbContext.Entry(eentity).CurrentValues.SetValues(entity);
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("BaseRepo UpdateAsync was Done for " + typeof(T).FullName);
                    return true;
                }
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "BaseRepo UpdateAsync was Failed for " + typeof(T).FullName);
                throw;
            }
        }
        public async Task<bool> DeleteAsync(long id)
        {
            logger.LogInformation("BaseRepo DeleteAsync was called for " + typeof(T).FullName);
            try
            {
                var entity = dbContext.Set<T>().Find(id);
                if (entity == null) throw new AccountingException("Not found ID", false, ErrorCodes.NotFound);
                entity.DeletedDate = DateTime.Now;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("BaseRepo DeleteAsync was Done for " + typeof(T).FullName);
                return true;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "BaseRepo DeleteAsync was Failed for " + typeof(T).FullName);
                throw;
            }
        }
    }
}