namespace EcoBar.Accounting.Data.Entities
{
    public class Payment : BaseEntity
    {
        public long AccountUserId { get; set; }
        public AccountUser? AccountUser { get; set; }

        public long Price { get; set; }
    }
}