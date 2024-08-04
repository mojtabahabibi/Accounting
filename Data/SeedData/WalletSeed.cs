using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class WalletSeed
    {
        public static Wallet GetWallet()
        {
            return new Wallet()
            {
                Id=1,
                AccountId = 1,
                WalletNumber = Guid.NewGuid(),
                Amount = 0,
            };
        }
    }
}
