namespace EcoBar.Accounting.Data.Entities
{
    public class AccountUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}