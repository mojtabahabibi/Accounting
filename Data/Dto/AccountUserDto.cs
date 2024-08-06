namespace EcoBar.Accounting.Data.Dto
{
    public class CreateAccountUserDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
    }
    public class AccountUserListDto
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
    }
    public class AccountUserListResponseDto : BaseResponseDto<List<AccountUserListDto>>
    {

    }
}