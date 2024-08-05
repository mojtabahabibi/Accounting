namespace EcoBar.Accounting.Data.Dto
{
    public class CreateInvoiceDto
    {
        public long AccountUserId { get; set; }
        public string? Title { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime Date { get; set; }
    }
    public class UpdateInvoiceDto
    {
        public long Id { get; set; }
        public long AccountUserId { get; set; }
        public string? Title { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime Date { get; set; }
    }
    public class DeleteInvoiceDto
    {
        public long Id { get; set; }
    }
    public class CloseInvoiceDto
    {
        public long Id { get; set; }
    }
    // public class PaymentInvoiceDto
    // {
    //     public long Id { get; set; }
    // }
    public class InvoiceListDto
    {
        public long AccountUserId { get; set; }
        public long InvoiceId { get; set; }
        public string? Title { get; set; }
        public string? SerialNumber { get; set; }
        public long Price { get; set; }
        public long Off { get; set; }
        public long TotalPrice { get; set; }
        public required string Status { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceItemDetailsResponseDto>? InvoiceItems { get; set; }
    }
    public class GetAllInvoiceResponseDto : BaseResponseDto<List<InvoiceListDto>>
    {

    }
    public class GetByIdInvoiceResponseDto : BaseResponseDto<InvoiceListDto>
    {

    }
}