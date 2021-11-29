using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderProductModel : ModelClass
    {
        public Guid ID { get; private set; }

        public Guid ProductId { get; private set; }
        public ProductModel Product { get; private set; }
        
        public int Quantity { get; private set; }
        
        public Guid OrderId { get; private set; }
        public OrderModel Order { get; private set; }

        public OrderProductModel(Guid productId, int quantity, Guid orderId, Guid? id = null)
        {
            ID = id ?? Guid.NewGuid();

            ProductId = productId;

            Quantity = quantity;

            OrderId = orderId;
        }

        public OrderProductModel(Guid productId, ProductModel product, int quantity, Guid orderId, OrderModel orderModel, Guid? id = null)
        {
            ID = id ?? Guid.NewGuid();

            ProductId = productId;
            Product = product;

            Quantity = quantity;

            OrderId = orderId;
            Order = orderModel;
        }

        public override OrderProductEntity ToEntity()
        {
            return new OrderProductEntity(ID, ProductId, Product?.ToEntity(), Quantity, OrderId, Order?.ToEntity());
        }
    }
}
