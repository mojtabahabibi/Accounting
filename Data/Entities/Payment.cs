namespace EcoBar.Accounting.Data.Entities
{
    public class Payment : BaseEntity
    {
        public long AccountId { get; set; }
        public required Account Account { get; set; }
        public long Price { get; set; }
    }
}