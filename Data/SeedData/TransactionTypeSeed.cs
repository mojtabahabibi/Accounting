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
                Title="واریز به حساب نقدی",
            },
             new TransactionType()
            {
                Id = 2,
                Title="خرید از حساب نقدی",
            },
              new TransactionType()
            {
                Id = 3,
                Title="واریز به حساب کیف پول",
            },
               new TransactionType()
            {
                Id = 4,
                Title="خرید از حساب کیف پول",
            },
               new TransactionType()
            {
                Id = 5,
                Title="واریز به حساب صندوق",
            },
               new TransactionType()
            {
                Id = 6,
                Title="خرید از حساب صندوق",
            },
              new TransactionType()
            {
                Id = 7,
                Title="مرجوعی",
            }
            };
        }
    }
}