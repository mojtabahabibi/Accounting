namespace EcoBar.Accounting.Data.Entities
{
    public class AccountTransaction : BaseEntity
    {
        public long? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public long? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        public long TransactionTypeId { get; set; }
        public TransactionType? TransactionType { get; set; }

        public string? AccountNumber { get; set; }
        public string? AccountUsername { get; set; }
        public string? TrackingNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public required string TransactionNumber { get; set; }
    }
}