namespace EcoBar.Accounting.Data.Entities
{
    public class Account : BaseEntity
    {
        public long AccountUserId { get; set; }
        public AccountUser? AccountUser { get; set; }
        public string Title { get; set; }
        public string AccountNumber { get; set; }
    }
}