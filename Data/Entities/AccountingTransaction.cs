using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Entities
{
    public class AccountingTransaction : BaseEntity
    {
        public long PayId { get; set; }
        public long FactorDetailId { get; set; }
        public virtual AccountingFactorDetails FactorDetail { get; set; }
        public PaymentTransActionState State { get; set; }
    }
}