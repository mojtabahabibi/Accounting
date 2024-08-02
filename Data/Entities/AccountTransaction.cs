namespace EcoBar.Accounting.Data.Entities
{
    public class AccountTransaction : BaseEntity
    {
        public long? InvoiceId { get; set; }
        public Invoice? Invocie { get; set; }
        public long? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        public Guid TransactionNumber { get; set; }
        public DateTime Time { get; set; }
    }
}