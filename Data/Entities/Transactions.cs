namespace EcoBar.Accounting.Data.Entities
{
    public class Transactions : BaseEntity
    {
        public long? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public long? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        public long TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; }

        public long RefrenceId { get; set; }
        public string? AccountNumber { get; set; }
        public string? Username { get; set; }
        public string? TrackingNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public required string TransactionNumber { get; set; }
        public long Debtor { get; set; }
        public long Creditor { get; set; }
    }
}