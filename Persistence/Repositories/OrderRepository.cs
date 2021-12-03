using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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
            _context.Entry(entity).State = EntityState.Unchanged;
        }

        public async Task<OrderModel> GetById(Guid id)
        {
            var entity = await _context.Orders.Where(o => o.ID == id).AsNoTracking().FirstOrDefaultAsync();
            return entity?.ToModel();
        }

        public async Task Update(OrderModel orderModel)
        {
            var entity = orderModel.ToEntity();

            _context.Attach(entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
