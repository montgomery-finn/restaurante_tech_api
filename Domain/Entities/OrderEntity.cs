using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderEntity : EntityClass
    {
        public Guid ID { get; set; }
        public Guid? CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public List<OrderProductEntity> OrderProducts { get; set; }

        public OrderEntity()
        {

        }

        public OrderEntity(Guid id, CustomerEntity customer, List<OrderProductEntity> orderProducts)
        {
            ID = id;
            CustomerId = customer?.ID;
            Customer = customer;
            OrderProducts = orderProducts;
        }

        public override OrderModel ToModel()
        {
            return new OrderModel(Customer.ToModel(), OrderProducts.Select(o => o.ToModel()).ToList(), ID);
        }
    }
}
