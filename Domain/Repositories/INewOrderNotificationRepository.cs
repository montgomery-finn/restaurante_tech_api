using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface INewOrderNotificationRepository
    {
        public Task<List<NewOrderNotificationModel>> GetAll();
        public Task Add(NewOrderNotificationModel newOrderNotificationModel);
        public Task Remove(NewOrderNotificationModel newOrderNotificationModel);
        public Task LoadOrder(NewOrderNotificationModel newOrderNotificationModel);
    }
}
