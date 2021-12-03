using System;

namespace Domain.Models
{
    public class NewOrderNotification
    {
        public Guid ID { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public NewOrderNotification() { }

        public NewOrderNotification(Guid orderId, Order order)
        {
            ID = Guid.NewGuid();
            OrderId = orderId;
            Order = order;
        }
    }
}
