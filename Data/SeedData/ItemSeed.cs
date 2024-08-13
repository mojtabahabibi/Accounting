using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class ItemSeed
    {
        public static Item GetItem()
        {
            return new Item()
            {
                Id = 1,
                Code = "1",
                Name = "خرید شارژ",
                Price = 10000
            };
        }
    }
}