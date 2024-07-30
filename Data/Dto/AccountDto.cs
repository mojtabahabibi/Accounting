namespace EcoBar.Accounting.Data.Dto
{
    public class BaseAccountDto
    {
        public long BaseUserId { get; set; }
        public string Title { get; set; }
        public string AccountNumber { get; set; }
    }
    public class BaseAccountIdDto
    {
        public long Id { get; set; }
    }
    public class UpdateAccountDto : BaseAccountDto
    {
        public long Id { get; set; }
    }
    public class AccountCreateGetResponseDto : BaseResponseDto<List<BaseAccountDto>>
    {

    }
    public class AccountGetByIdResponseDto : BaseResponseDto<BaseAccountDto>
    {

    }
}