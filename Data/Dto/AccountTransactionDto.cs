namespace EcoBar.Accounting.Data.Dto
{
    public class AccountTransactionUserNameDto
    {
        public required string AccountUserName { get; set; }
    }
    public class AccountTransactionNumberDto
    {
        public required string TransactionNumber { get; set; }
    }
    public class AccountTransactionListDto
    {
        public long? AccountId { get; set; }
        public string? TransactionType { get; set; }
        public string? AccountNumber { get; set; }
        public string? UserName { get; set; }
        public long Price { get; set; }
        public string? TrackingNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? TransactionNumber { get; set; }
        public DateTime Time { get; set; }
        public string? Description { get; set; }
    }
    public class AccountTranasctionGetAllResponseDto : BaseResponseDto<List<AccountTransactionListDto>>
    {

    }
    public class AccountTranasctionGetByAccountIdResponseDto : BaseResponseDto<List<AccountTransactionListDto>>
    {

    }
    public class AccountTranasctionGetByUsernameResponseDto : BaseResponseDto<List<AccountTransactionListDto>>
    {

    }
}