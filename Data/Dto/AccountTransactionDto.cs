namespace EcoBar.Accounting.Data.Dto
{
    public class AccountTransactionUserNameDto
    {
        public required string AccountUserName { get; set; }
    }
    public class AccountTransactionNumberDto
    {
        public required Guid TransactionNumber { get; set; }
    }
    public class AccountTransactionListDto
    {
        public required  string TransactionType { get; set; }
        public required  string AccountUserName { get; set; }
        public required  long Price { get; set; }
        public required  Guid TransactionNumber { get; set; }
        public required  DateTime Time { get; set; }
    }
    public class AccountTranasctionGetAllResponseDto : BaseResponseDto<List<AccountTransactionListDto>>
    {

    }
    public class AccountTranasctionGetByUsernameResponseDto : BaseResponseDto<AccountTransactionListDto>
    {

    }
}