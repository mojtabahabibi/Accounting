namespace EcoBar.Accounting.Data.Dto
{
    public class BaseAccountDto
    {
        public long Id { get; set; }
        public long AccountUserId { get; set; }
        public string? Title { get; set; }
        public string? AccountNumber { get; set; }
        public long Amount { get; set; }
    }
    public class BaseAccountIdDto
    {
        public long Id { get; set; }
    }
    public class UpdateAccountDto : BaseAccountDto
    {
        
    }
    public class AccountCreateGetResponseDto : BaseResponseDto<List<BaseAccountDto>>
    {

    }
    public class AccountGetByIdResponseDto : BaseResponseDto<BaseAccountDto>
    {

    }
}