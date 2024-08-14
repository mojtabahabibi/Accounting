using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Entities
{
    public class InvoiceX : BaseEntity
    {
        public long InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}