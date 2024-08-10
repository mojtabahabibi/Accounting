using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class AccountUserSeed
    {
        public static AccountUser GetAccountUser()
        {
            return new AccountUser()
            {
                Id = 1,
                UserName = "Company",
                Password = "123456",
            };
        }
    }
}
