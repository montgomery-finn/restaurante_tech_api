using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

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
            return _context.Orders.Where(o => o.ID == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task Update(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
