namespace EcoBar.Accounting.Data.Entities
{
    public class Item :BaseEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required long Price { get; set; }
    }
}