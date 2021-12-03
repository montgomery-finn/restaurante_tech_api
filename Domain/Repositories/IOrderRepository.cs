using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        public Task Add(Order order);
        public Task<Order> GetById(Guid id);
        public Task Update(Order order);
    }
}
