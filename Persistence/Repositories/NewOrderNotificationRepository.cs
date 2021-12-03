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
    public class NewOrderNotificationRepository : INewOrderNotificationRepository
    {
        private readonly TechContext _context;

        public NewOrderNotificationRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(NewOrderNotificationModel newOrderNotificationModel)
        {
            var entity = newOrderNotificationModel.ToEntity();
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Unchanged;
        }

        public async Task<List<NewOrderNotificationModel>> GetAll()
        {
            var entities = await _context.NewOrderNotifications.AsNoTracking().ToListAsync();
            var models = entities.Select(e => e.ToModel()).ToList();
            return models;
        }

        public async Task Remove(NewOrderNotificationModel newOrderNotificationModel)
        {
            var entity = newOrderNotificationModel.ToEntity();
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task LoadOrder(NewOrderNotificationModel newOrderNotificationModel)
        {
            var entity = newOrderNotificationModel.ToEntity();

            entity.Order = await _context.Orders.Where(o => o.ID == entity.OrderId).AsNoTracking().FirstOrDefaultAsync();
            entity.Order.Customer = await _context.Customers.Where(c => c.ID == entity.Order.CustomerId).AsNoTracking().FirstOrDefaultAsync();
            entity.Order.OrderProducts = await _context.OrderProducts.Where(o => o.OrderId == entity.Order.ID).AsNoTracking().ToListAsync();

            foreach(var orderProduct in entity.Order.OrderProducts)
            {
                orderProduct.Product = await _context.Products.Where(p => p.ID == orderProduct.ProductId).AsNoTracking().FirstOrDefaultAsync();
            }

            newOrderNotificationModel.Order = entity.Order.ToModel();
        }
    }
}
