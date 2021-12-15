using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using System.Collections.Generic;

namespace Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TechContext _context;

        public OrderRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(Order order)
        {
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public Task<Order> GetById(Guid id)
        {
            return _context.Orders.Where(o => o.ID == id).FirstOrDefaultAsync();
        }

        public async Task Update(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task LoadCustomer(Order order)
        {
            await _context.Entry(order).Reference(o => o.Customer).LoadAsync();
        }

        public async Task LoadProducts(Order order)
        {
            await _context.Entry(order).Collection(o => o.OrderProducts).Query().Include(op => op.Product).LoadAsync();

        }

        public Task<List<Order>> GetAll()
        {
            return _context.Orders.ToListAsync();
        }

        public async Task LoadUser(Order order)
        {
            await _context.Entry(order).Reference(o => o.User).LoadAsync();
        }

        public Task<List<Order>> GetFinished()
        {
            return _context.Orders.Where(o => o.Finished).ToListAsync();
        }
    }
}
