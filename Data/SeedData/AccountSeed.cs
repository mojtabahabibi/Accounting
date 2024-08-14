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
                    UserId = 1,
                    AccountTypeId=1,
                    Title = "حساب نقدی صندوق",
                    AccountNumber ="00000000",
                    Amount=0,
                }
            };
        }
    }
}