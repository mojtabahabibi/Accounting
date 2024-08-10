namespace EcoBar.Accounting.Data.Entities
{
    public class AccountType : BaseEntity
    {
        public required string Type { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}