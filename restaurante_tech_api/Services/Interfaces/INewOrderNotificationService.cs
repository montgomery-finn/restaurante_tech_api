using Domain.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace restaurante_tech_api.Services.Interfaces
{
    public interface INewOrderNotificationService
    {
        void AddClient(StreamWriter client);
        Task AddNotification(NewOrderNotification newOrderNotification);
        Task RemoveNotificationFromOrder(Guid orderId);
        void TryTake();
    }
}