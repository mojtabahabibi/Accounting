namespace EcoBar.Accounting.Data.Entities
{
    public class Company : BaseEntity
    {
        public long UserId { get; set; }
        public User? User { get; set; }

        public required string Name { get; set; }
        public required string Economicalnumber { get; set; }
    }
}