using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class AccountSeed
    {
        public static Account GetAccount()
        {
            return new Account()
            {
                Id = 1,
                AccountUserId = 1,
                AccountNumber = "123",
                Title = "حساب صندوق"
            };
        }
    }
}