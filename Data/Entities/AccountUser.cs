namespace EcoBar.Accounting.Data.Entities
{
    public class AccountUser : BaseEntity
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
    }
}