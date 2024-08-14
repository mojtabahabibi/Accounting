using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Repo.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> CreateUserAsync(User model);
    }
}
