namespace EcoBar.Accounting.Data.Entities
{
    public class Wallet : BaseEntity
    {
        public Guid WalletNumber { get; set; }
        public long Amount { get; set; }

        public Account? Account { get; set; }
    }
}