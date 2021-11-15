using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductModel : ModelClass
    {
        public Guid ID { get; set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public int PriceInPoints { get; private set; }
        public string FileName { get; private set; }

        public ProductModel(string name, double price, int priceInPoints, string fileName, Guid? id = null)
        {
            ID = id == null ? Guid.NewGuid() : id.Value;
            Name = name;
            Price = price;
            PriceInPoints = priceInPoints;
            FileName = fileName;
        }

        public override ProductEntity ToEntity()
        {
            return new ProductEntity(Name, Price, PriceInPoints, FileName, ID);
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
