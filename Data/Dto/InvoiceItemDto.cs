namespace EcoBar.Accounting.Data.Dto
{
    public class BaseInvoiceItemDto
    {
        public long ItemId { get; set; }
        public string? Name { get; set; }
        public int Count { get; set; }
        public long Off { get; set; }
        public long Price { get; set; }
        public long InvoiceId { get; set; }
    }
    public class BaseInvoiceItemIdDto
    {
        public long Id { get; set; }
    }
    public class UpdateInvoiceItemDto
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public int Count { get; set; }
        public long Off { get; set; }
    }
    public class DeleteInvoiceItemDto : BaseInvoiceItemIdDto
    {

    }

    public class InvoiceItemDetailsResponseDto
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public string? ItemName { get; set; }
        public int Count { get; set; }
        public long Off { get; set; }
        public long Price { get; set; }
    }
    public class InvioceItemGetAllResponseDto : BaseResponseDto<List<BaseInvoiceItemDto>>
    {

    }
    public class InvioceItemGetByIdResponseDto : BaseResponseDto<BaseInvoiceItemDto>
    {

    }
}