using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class UserSeed
    {
        public static User GetUser()
        {
            return new User()
            {
                Id = 1,
                UserName = "Company",
                Password = "123456",
            };
        }
    }
}
