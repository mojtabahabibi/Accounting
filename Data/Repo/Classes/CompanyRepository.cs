using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class CompanyRepository : BaseRepo<Company>, ICompanyRepository
    {
        public CompanyRepository(AccountingDbContext dbContext, ILogger<BaseRepo<Company>> logger) : base(dbContext, logger)
        {

        }
        public async Task<Company> AddComapnyAsync(Company entity)
        {
            logger.LogInformation("CompanyRepository AddAsync was called for ");
            try
            {
                var user = new AccountUser()
                {
                    UserName = "Comapny",
                    Password = entity.Economicalnumber,
                    Name = entity.Name
                };
                await dbContext.AccountUsers.AddAsync(user);
                await dbContext.SaveChangesAsync();

                entity.AccountUserId = user.Id;
                await dbContext.Companies.AddAsync(entity);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("CompanyRepository AddAsync was Done for ");
                return entity;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "CompanyRepository AddAsync was Failed for ");
                throw;
            }
        }
    }
}
