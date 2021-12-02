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
            _context.Attach(entity);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task LoadOrder(NewOrderNotificationModel newOrderNotificationModel)
        {
            var entity = newOrderNotificationModel.ToEntity();
            _context.Attach(entity);

            await _context.Entry(entity).Reference(e => e.Order).Query()
                        .Include(o => o.Customer)
                        .Include(o => o.OrderProducts).ThenInclude(op => op.Product).LoadAsync();

            newOrderNotificationModel.Order = entity.Order.ToModel();
        }
    }
}
