using System;

namespace Domain.Models
{
    public class OrderProduct
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid OrderId { get; set; }

        public OrderProduct()
        {

        }

        public OrderProduct(Guid productId, Guid orderId)
        {
            ID = Guid.NewGuid();
            ProductId = productId;
            OrderId = orderId;
        }
    }
}
