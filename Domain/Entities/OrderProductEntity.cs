using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderProductEntity : EntityClass
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }

        public OrderProductEntity()
        {

        }

        public OrderProductEntity(Guid id, Guid productId, ProductEntity product, int quantity, Guid orderId, OrderEntity order)
        {
            ID = id;

            ProductId = productId;
            Product = product;
            
            Quantity = quantity;
            
            OrderId = orderId;
            Order = order;

        }

        public override OrderProductModel ToModel()
        {
            return new OrderProductModel(ProductId, Product?.ToModel(), Quantity, OrderId, Order.ToModel(), ID);
        }
    }
}
