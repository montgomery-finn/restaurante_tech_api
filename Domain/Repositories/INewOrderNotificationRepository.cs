using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface INewOrderNotificationRepository
    {
        public Task<List<NewOrderNotification>> GetAll();
        public Task Add(NewOrderNotification newOrderNotification);
        public Task Remove(NewOrderNotification newOrderNotification);
        public Task LoadOrder(NewOrderNotification newOrderNotification);
    }
}
