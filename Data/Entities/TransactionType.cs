namespace EcoBar.Accounting.Data.Entities
{
    public class TransactionType : BaseEntity
    {
        public required string Title { get; set; }

        public ICollection<AccountTransaction>? AccountTransactions { get; set; }
    }
}