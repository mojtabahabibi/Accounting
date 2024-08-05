namespace EcoBar.Accounting.Data.Entities
{
    public class Company : BaseEntity
    {
        public long AccountUserId { get; set; }
        public AccountUser? AccountUser { get; set; }

        public required string Name { get; set; }
        public required string Economicalnumber { get; set; }
    }
}