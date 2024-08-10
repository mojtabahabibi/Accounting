namespace EcoBar.Accounting.Data.Entities
{
    public class AccountTransaction : BaseEntity
    {
        public long TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; }

        public Guid TransactionNumber { get; set; }

        public Invoice? Invoice { get; set; }
        public Payment? Payment { get; set; }

        public ICollection<AccountBook>? AccountBooks { get; set; }
    }
}