namespace EcoBar.Accounting.Data.Entities
{
    public class AccountingFactor :BaseEntity
    {
        public long PaymentDocumentID { get; set; }
        public virtual AccountingDocument PaymentDocument { get; set; }
        public virtual  ICollection<AccountingFactorDetails> FactorDetails { get; set; }
    }
}