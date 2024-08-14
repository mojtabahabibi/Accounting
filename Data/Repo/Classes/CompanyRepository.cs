using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AccountingDbContext dbContext, ILogger<BaseRepository<Company>> logger) : base(dbContext, logger)
        {

        }
        public async Task<Company> AddComapnyAsync(Company entity)
        {
            logger.LogInformation("CompanyRepository AddAsync was called for ");
            try
            {
                var user = new User()
                {
                    UserName = "Comapny",
                    Password = entity.Economicalnumber,
                };
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();

                entity.UserId = user.Id;
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
