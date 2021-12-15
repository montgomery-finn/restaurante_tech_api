using System;

namespace Domain.Models
{
    public class Product
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int PriceInPoints { get; set; }
        public string FileName { get; set; }

        public Product() { }

        public Product(string name, double price, int priceInPoints, string fileName)
        {
            ID = Guid.NewGuid();
            Name = name;
            Price = price;
            PriceInPoints = priceInPoints;
            FileName = fileName;
        }

        public void Update(string name, double price, int priceInPoints, string fileName = null)
        {
            Name = name;
            Price = price;
            PriceInPoints = priceInPoints;
            if(fileName != null)
            {
                FileName = fileName;
            }
        }
    }
}
