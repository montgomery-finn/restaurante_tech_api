using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Order
    {
        public Guid ID { get; set; }
        public bool Finished { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public Guid? UserId { get; set; }
        public User User { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public Order()
        {

        }

        public Order(Guid? customerId)
        {
            ID = Guid.NewGuid();
            CustomerId = customerId;
            CreatedAt = DateTime.Now;
        }

        public void Finish(Guid userId)
        {
            Finished = true;
            UserId = userId;
        }
    }
}
