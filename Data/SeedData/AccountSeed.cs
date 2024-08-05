using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class AccountSeed
    {
        public static List<Account> GetAccountWallet()
        {
            return new List<Account>()
            {
              new Account()
            {
                Id = 1,
                AccountUserId = 1,
                AccountNumber = "123",
                Title = "حساب نقدی صندوق",
                AccountTypeId=1,
            },
                new Account()
            {
                Id = 2,
                AccountUserId = 1,
                AccountNumber = "123",
                Title = "حساب کیف پول صندوق",
                AccountTypeId=2,
            }
            };
        }
    }
}