using Domain.Models;
using System.IO;
using System.Threading.Tasks;

namespace restaurante_tech_api.Services.Interfaces
{
    public interface INewOrderNotificationService
    {
        void AddClient(StreamWriter client);
        Task AddNotification(NewOrderNotificationModel newOrderNotification);
        Task RemoveNotification(NewOrderNotificationModel newOrderNotification);
        void TryTake();
    }
}