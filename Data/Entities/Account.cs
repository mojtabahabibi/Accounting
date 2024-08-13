namespace EcoBar.Accounting.Data.Entities
{
    public class Account : BaseEntity
    {
        public long AccountUserId { get; set; }
        public AccountUser? AccountUser { get; set; }

        public long AccountTypeId { get; set; }
        public AccountType? AccountType { get; set; }

        public required string Title { get; set; }
        public required string AccountNumber { get; set; }
        public long Amount { get; set; }

        public ICollection<Payment>? Payments { get; set; }
    }
}