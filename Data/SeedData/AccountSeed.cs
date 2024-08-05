using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class AccountSeed
    {
        public static List<Account> GetAccountWallet()
        {
            Random random = new Random();
            return new List<Account>()
            {
              new Account()
            {
                Id = 1,
                AccountUserId = 1,
                AccountNumber =random.Next(9999999,100000000).ToString(),
                Title = "حساب نقدی صندوق",
                AccountTypeId=1,
            }
            };
        }
    }
}