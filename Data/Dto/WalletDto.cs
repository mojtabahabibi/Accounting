namespace EcoBar.Accounting.Data.Dto
{
    public class WalletListDto
    {
        public required string AccountUser { get; set; }
        public required string AccountNumber { get; set; }
        public Guid WalletNumber { get; set; }
        public long Amount { get; set; }
    }
    public class WalletGetByusernameDto
    {
        public required string Username { get; set; }
    }
    public class WalletGetAllResponseDto : BaseResponseDto<List<WalletListDto>>
    {

    }
    public class WalletGetByUsernameResponseDto : BaseResponseDto<WalletListDto>
    {

    }
}
