using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Data.Repo.Classes
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(AccountingDbContext dbContext, ILogger<BaseRepository<Item>> logger) : base(dbContext, logger)
        {
        }
    }
}