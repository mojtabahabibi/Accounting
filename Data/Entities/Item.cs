namespace EcoBar.Accounting.Data.Entities
{
    public class Item : BaseEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public long Price { get; set; }
    }
}