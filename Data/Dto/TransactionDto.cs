namespace EcoBar.Accounting.Data.Dto
{
    public class TransactionsUserNameDto
    {
        public required string UserName { get; set; }
    }
    public class TransactionsNumberDto
    {
        public required string TransactionNumber { get; set; }
    }
    public class TransactionsListDto
    {
        public long? AccountId { get; set; }
        public required long TransactionId { get; set; }
        public required long RefrenceId { get; set; }
        public string? TransactionType { get; set; }
        public string? AccountNumber { get; set; }
        public string? UserName { get; set; }
        public long Debtor { get; set; }
        public long Creditor { get; set; }
        public string? TrackingNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? TransactionNumber { get; set; }
        public DateTime Time { get; set; }
        public string? Description { get; set; }
    }
    public class AccountTranasctionGetAllResponseDto : BaseResponseDto<List<TransactionsListDto>>
    {

    }
    public class AccountTranasctionGetByAccountIdResponseDto : BaseResponseDto<List<TransactionsListDto>>
    {

    }
    public class AccountTranasctionGetByUsernameResponseDto : BaseResponseDto<List<TransactionsListDto>>
    {

    }
}