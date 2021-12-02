using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NewOrderNotificationEntity : EntityClass
    {
        public Guid ID { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }

        public NewOrderNotificationEntity() { }

        public NewOrderNotificationEntity(Guid id, Guid orderId, OrderEntity order)
        {
            ID = id;
            OrderId = orderId;
            Order = order;
        }

        public override NewOrderNotificationModel ToModel()
        {
            return new NewOrderNotificationModel(OrderId, Order?.ToModel(), ID);
        }
    }
}
