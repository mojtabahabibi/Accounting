namespace EcoBar.Accounting.Data.Entities
{
    public class Payment : BaseEntity
    {
        public long AccountId { get; set; }
        public required Account Account { get; set; }

        public long InvoicePayTypeId { get; set; }
        public required InvoicePayType InvoicePayType { get; set; }

        public long TransactionId { get; set; }
        public long Price { get; set; }

        public ICollection<AccountTransaction>? AccountTransactions { get; set; }
    }
}