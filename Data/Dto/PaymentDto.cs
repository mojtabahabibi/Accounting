namespace EcoBar.Accounting.Data.Dto
{
    public class CreatePaymentDto
    {
        public long AccountId { get; set; }
        public long Price { get; set; }
    }
    public class PaymentInvoiceDto
    {
        public long InvoiceId { get; set; }
        public long AccountId { get; set; }
    }
    public class TransferDto
    {
        public long AccountCashId { get; set; }
        public long AccountWalletId { get; set; }
        public long Price { get; set; }
    }
}