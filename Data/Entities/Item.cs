namespace EcoBar.Accounting.Data.Entities
{
    public class Item : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
    }
}