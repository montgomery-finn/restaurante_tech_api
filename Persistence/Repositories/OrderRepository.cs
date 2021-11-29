using Domain.Models;
using Domain.Repositories;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TechContext _context;

        public OrderRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(OrderModel order)
        {
            var entity = order.ToEntity();
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
