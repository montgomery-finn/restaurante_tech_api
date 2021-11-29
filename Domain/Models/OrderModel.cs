using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderModel : ModelClass
    {
        public Guid ID { get; private set; }
        public CustomerModel Customer { get; private set; }

        private List<OrderProductModel> _orderProducts;
        public IReadOnlyList<OrderProductModel> OrderProducts { get { return _orderProducts; } }

        public OrderModel(CustomerModel customer, List<OrderProductModel> orderProducts, Guid? id = null)
        {
            ID = id ?? Guid.NewGuid();
            Customer = customer;
            _orderProducts = orderProducts ?? new List<OrderProductModel>();
        }

        public void AddProduct(ProductModel product, int quantity)
        {
            var orderProduct = new OrderProductModel(product.ID, quantity, ID);
            _orderProducts.Add(orderProduct);
        }

        public override OrderEntity ToEntity()
        {
            return new OrderEntity(ID, Customer?.ToEntity(), OrderProducts.Select(o => o.ToEntity()).ToList());
        }
    }
}
