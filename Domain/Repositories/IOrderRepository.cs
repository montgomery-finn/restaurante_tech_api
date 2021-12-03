using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        public Task Add(OrderModel order);
        public Task<OrderModel> GetById(Guid id);
        public Task Update(OrderModel orderModel);
    }
}
