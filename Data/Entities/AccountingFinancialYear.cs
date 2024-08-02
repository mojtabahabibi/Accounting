namespace EcoBar.Accounting.Data.Entities
{
    public class AccountingFinancialYear :BaseEntity
    {
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string  Title { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
    }
}