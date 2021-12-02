using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class NewOrderNotificationModel : ModelClass
    {
        public Guid ID { get; private set; }
        public Guid OrderId { get; private set; }
        public OrderModel Order { get; set; }

        public NewOrderNotificationModel(Guid orderId, OrderModel orderModel, Guid? id = null)
        {
            ID = id ?? Guid.NewGuid();
            OrderId = orderId;
            Order = orderModel;
        }

        public override NewOrderNotificationEntity ToEntity()
        {
            return new NewOrderNotificationEntity(ID, OrderId, Order?.ToEntity());
        }
    }
}
