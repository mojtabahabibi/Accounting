namespace EcoBar.Accounting.Data.Entities
{
    public class Account : BaseEntity
    {
        public long BaseUserId { get; set; }
        public string Title { get; set; }
        public string AccountNumber { get; set; }
    }
}