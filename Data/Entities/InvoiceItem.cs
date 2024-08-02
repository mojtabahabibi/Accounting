namespace EcoBar.Accounting.Data.Entities
{
    public class InvoiceItem : BaseEntity
    {
        public long ItemId { get; set; }
        public required virtual Item Item { get; set; }
        public required string Name { get; set; }
        public int Count { get; set; }
        public long Off { get; set; }
        public required long Price { get; set; }
        public long InvoiceId { get; set; }
        public required Invoice Invoice { get; set; }
    }
}