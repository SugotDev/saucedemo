using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2E.Models
{
    public class Product
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Price { get; init; }
        public string ImageSrc { get; init; }

        public Product(string name, string description, string price, string imageSrc)
        {
            Name = name;
            Description = description;
            Price = price;
            ImageSrc = imageSrc;
        }

        public override string ToString()
        {
            return $"{Name} ({Price})";
        }
    }
}
