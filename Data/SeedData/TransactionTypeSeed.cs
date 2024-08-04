using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class TransactionTypeSeed
    {
        public static List<TransactionType> GetTransactionTypes()
        {
            return new List<TransactionType>()
            {
            new TransactionType()
            {
                Id = 1,
                Title="واریز به حساب",
            },
             new TransactionType()
            {
                Id = 2,
                Title="خرید از حساب",
            }
            };
        }
    }
}