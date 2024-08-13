namespace EcoBar.Accounting.Data.Entities
{
    public class AccountUser : BaseEntity
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public Company? Company { get; set; }
        public ICollection<Account>? Accounts { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
    }
}