namespace EcoBar.Accounting.Data.Entities
{
    public class AccountingFactorDetails : BaseEntity
    {
        public long PaymentFactorID { get; set; }
        public virtual AccountingFactor PaymentFactor { get; set; }
    }
}