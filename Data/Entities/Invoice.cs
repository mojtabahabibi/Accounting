using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Entities
{
    public class Invoice : BaseEntity
    {
        public long AccountUserId { get; set; }
        public AccountUser? AccountUser { get; set; }

        public long? AccountTransactionId { get; set; }
        public AccountTransaction? AccountTransaction { get; set; }

        public string? Title { get; set; }
        public string? SerialNumber { get; set; }
        public long Price { get; set; }
        public long Off { get; set; }
        public long TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public InvoiceStatus Status { get; set; }

        public ICollection<InvoiceItem>? InvoiceItems { get; set; }
    }
}