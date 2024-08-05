using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.SeedData
{
    public class ItemSeed
    {
        public static List<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item() {
                    Id=1,
                    Name="خرید شارژ",
                    Code="1",
                    Price=1000,
                    },
            };
        }
    }
}