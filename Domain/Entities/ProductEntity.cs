using Domain.Models;
using System;

namespace Domain.Entities
{
    public class ProductEntity : EntityClass
    {
        public Guid ID { get; set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int PriceInPoints { get; private set; }
        public string FileName { get; private set; }

        public ProductEntity() { }

        public ProductEntity(string name, decimal price, int priceInPoints, string fileName, Guid id)
        {
            ID = id;
            Name = name;
            Price = price;
            PriceInPoints = priceInPoints;
            FileName = fileName;
        }

        public override ProductModel ToModel()
        {
            return new ProductModel(Name, Price, PriceInPoints, FileName, ID);
        }
    }
}
