namespace EcoBar.Accounting.Data.Entities
{
    public class TransactionType : BaseEntity
    {
        public required string Title { get; set; }

        public ICollection<Transactions>? Transactionss { get; set; }
    }
}