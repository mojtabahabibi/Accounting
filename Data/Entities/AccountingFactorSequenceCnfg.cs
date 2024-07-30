namespace EcoBar.Accounting.Data.Entities
{
    public class AccountingFactorSequenceCnfg : BaseEntity
    {
        public string Title { get; set; }
        public string SeuenceConfig { get; set; }
        public int SequenceStartIndex { get; set; }
    }
}