using System;

namespace Domain.Models
{
    public class OrderProduct
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }

        public OrderProduct()
        {

        }

        public OrderProduct(Guid productId, int quantity, Guid orderId)
        {
            ID = Guid.NewGuid();

            ProductId = productId;
            
            Quantity = quantity;
            
            OrderId = orderId;
        }
    }
}
