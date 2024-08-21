using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Entities
{
    public class Invoice : BaseEntity
    {
        public long UserId { get; set; }
        public User? User { get; set; }

        public string? Title { get; set; }
        public string? SerialNumber { get; set; }
        public long Price { get; set; }
        public long Discount { get; set; }
        public long TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public InvoiceStatus Status { get; set; }

        public ICollection<Transactions>? Transactionss { get; set; }
        public ICollection<InvoiceItem>? InvoiceItems { get; set; }
        public ICollection<InvoiceX>? InvoiceXes { get; set; }
    }
}