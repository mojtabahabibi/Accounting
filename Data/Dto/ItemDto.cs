namespace EcoBar.Accounting.Data.Dto
{
    public class BaseItemDto
    {
        public string ?Code { get; set; }
        public string? Name { get; set; }
        public long Price { get; set; }
    }
    public class ItemListDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public long Price { get; set; }
    }
    public class GetAllItemResponseDto : BaseResponseDto<List<ItemListDto>>
    {

    }
}