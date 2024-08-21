namespace EcoBar.Accounting.Data.Dto
{
    public class CreateInvoiceDto
    {
        public long UserId { get; set; }
        public string? Title { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime Date { get; set; }
    }
    public class UpdateInvoiceDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string? Title { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime Date { get; set; }
    }
    public class InvoiceIdDto
    {
        public long Id { get; set; }
    }
    public class BuyChargeDto
    {
        public long UserId { get; set; }
        public long Price { get; set; }
    }
    public class PaymentChargeDto
    {
        public long UserId { get; set; }
        public long InvoiceId { get; set; }
    }
    public class InvoiceListDto
    {
        public long UserId { get; set; }
        public long InvoiceId { get; set; }
        public string? Title { get; set; }
        public string? SerialNumber { get; set; }
        public long Price { get; set; }
        public long Discount { get; set; }
        public long TotalPrice { get; set; }
        public required string Status { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceItemDetailsResponseDto>? InvoiceItems { get; set; }
    }
    public class InvoiceStatusDto
    {
        public DateTime ChangeTime { get; set; }
        public string? Status { get; set; }
    }
    public class InvoiceStatusListDto
    {
        public long InvoiceId { get; set; }
        public string? Title { get; set; }
        public string? SerialNumber { get; set; }
        public long? TotalPrice { get; set; }
        public required List<InvoiceStatusDto> InvoiceStatusDtos { get; set; }
    }
    public class GetAllInvoiceResponseDto : BaseResponseDto<List<InvoiceListDto>>
    {

    }
    public class GetByIdInvoiceResponseDto : BaseResponseDto<InvoiceListDto>
    {

    }
    public class InvoiceStatusListResponseDto: BaseResponseDto<InvoiceStatusListDto>
    {

    }
}