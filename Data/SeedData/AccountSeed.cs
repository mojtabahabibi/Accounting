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
                AccountTypeId=1,
                Title = "حساب نقدی صندوق",
                AccountNumber =random.Next(9999999,100000000).ToString(),
                Amount=0,
            }
            };
        }
    }
}