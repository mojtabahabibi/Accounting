namespace EcoBar.Accounting.Data.Entities
{
    public class InvoicePayType : BaseEntity
    {
        public required string Type { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}