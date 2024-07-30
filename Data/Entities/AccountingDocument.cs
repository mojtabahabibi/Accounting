namespace EcoBar.Accounting.Data.Entities
{
    public class AccountingDocument :BaseEntity
    {
        public string Title { get; set; }
        public virtual  ICollection<AccountingFactor> Factors { get; set; }
    }
}

/// سند حسابدرای می تواند شامل فاکتورها ، رسیدها ،برگه های بانکی است