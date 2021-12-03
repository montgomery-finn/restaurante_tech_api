using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories
{
    public class NewOrderNotificationRepository : INewOrderNotificationRepository
    {
        private readonly TechContext _context;

        public NewOrderNotificationRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(NewOrderNotification newOrderNotification)
        {
            await _context.AddAsync(newOrderNotification);
            await _context.SaveChangesAsync();
        }

        public Task<List<NewOrderNotification>> GetAll()
        {
            return _context.NewOrderNotifications.ToListAsync();
        }

        public async Task Remove(NewOrderNotification newOrderNotification)
        {
            _context.Remove(newOrderNotification);
            await _context.SaveChangesAsync();
        }

        public async Task LoadOrder(NewOrderNotification newOrderNotification)
        {
            await _context.Entry(newOrderNotification)
                    .Reference(n => n.Order).Query()
                        .Include(o => o.Customer)
                        .Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                        .LoadAsync();
        }
    }
}
