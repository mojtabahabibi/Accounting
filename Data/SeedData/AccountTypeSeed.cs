using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class AccountTypeSeed
    {
        public static List<AccountType> GetAccountTypes()
        {
            return new List<AccountType>()
            {
                new AccountType() {Id=1, Type = "حساب نقدی"},
                new AccountType() {Id=2, Type = "حساب کیف پول"},
            };
        }
    }
}