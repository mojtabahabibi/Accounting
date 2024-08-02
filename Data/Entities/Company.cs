namespace EcoBar.Accounting.Data.Entities
{
    public class Company : BaseEntity
    {
        public long AccountUserId { get; set; }
        public AccountUser? AccountUser { get; set; }

        public string Name { get; set; }
        public string Economicalnumber { get; set; }
    }
}