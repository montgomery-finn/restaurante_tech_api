using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        public Task Add(Order order);
        public Task<Order> GetById(Guid id);
        public Task Update(Order order);
        public Task LoadCustomer(Order order);
        public Task LoadProducts(Order order);
        public Task LoadUser(Order order);
        public Task<List<Order>> GetAll();
        public Task<List<Order>> GetFinished();
    }
}
