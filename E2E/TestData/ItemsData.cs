using E2E.Models;

namespace E2E.TestData
{
    public static class ItemsData
    {
        public static readonly Product Backpack = new Product(
            "Sauce Labs Backpack",
            "carry.allTheThings() with the sleek, streamlined Sly Pack that melds uncompromising style with unequaled laptop and tablet protection.",
            "$29.99",
            "/static/media/sauce-backpack-1200x1500"
        );

        public static readonly Product BikeLight = new Product(
            "Sauce Labs Bike Light",
            "A red light isn't the desired state in testing but it sure helps when riding your bike at night.",
            "$9.99",
            "/static/media/bike-light-1200x1500"
        );

        public static readonly Product BoltTShirt = new Product(
            "Sauce Labs Bolt T-Shirt",
            "Get your testing superhero on with the Sauce Labs bolt T-shirt. From American Apparel, 100% ringspun combed cotton, heather gray with red bolt.",
            "$15.99",
            "/static/media/bolt-shirt-1200x1500"
        );

        public static readonly Product Jacket = new Product(
            "Sauce Labs Fleece Jacket",
            "carry.allTheThings() with the sleek, streamlined Sly Pack that melds uncompromising style with unequaled laptop and tablet protection.",
            "$29.99",
            "/static/media/sauce-pullover-1200x1500"
        );

        public static readonly Product Onesie = new Product(
            "Sauce Labs Onesie",
            "Rib snap infant onesie for the junior automation engineer in development. Reinforced 3-snap bottom closure, two-needle hemmed sleeved and bottom won't unravel.",
            "$7.99",
            "/static/media/red-onesie-1200x1500"
        );
    }
}
