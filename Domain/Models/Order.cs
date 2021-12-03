using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Order
    {
        public Guid ID { get; set; }
        public Guid? CustomerId { get; set; }
        public bool Finished { get; set; }
        public Customer Customer { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

        public Order()
        {

        }

        public Order(Customer customer)
        {
            ID = Guid.NewGuid();
            CustomerId = customer?.ID;
            Customer = customer;
        }

        public void Finish()
        {
            Finished = true;
        }
    }
}
