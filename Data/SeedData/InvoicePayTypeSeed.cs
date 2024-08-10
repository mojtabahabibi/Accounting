using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class InvoicePayTypeSeed
    {
        public static List<InvoicePayType> GetInvoicePayTypes()
        {
            return new List<InvoicePayType>()
            {
                new InvoicePayType
                {
                    Id=1,
                    Type="نقدی"
                },
                new InvoicePayType
                {
                    Id=2,
                    Type="کیف پول"
                }
            };
        }
    }
}