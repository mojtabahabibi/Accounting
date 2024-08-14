using System.Transactions;

namespace EcoBar.Accounting.Data.Dto
{
    public class CreateUserDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    public class UserListDto
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    public class UserListResponseDto : BaseResponseDto<List<UserListDto>>
    {

    }
}