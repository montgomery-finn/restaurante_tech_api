using Domain.Models;
using Domain.Repositories;
using restaurante_tech_api.Services.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace restaurante_tech_api.Services
{
    public class NewOrderNotificationService : INewOrderNotificationService
    {
        private readonly INewOrderNotificationRepository _newOrderNotificationRepository;
        private ConcurrentBag<StreamWriter> _clients;
        private List<NewOrderNotificationModel> _newOrderNotifications;

        public NewOrderNotificationService(INewOrderNotificationRepository newOrderNotificationRepository)
        {
            _newOrderNotificationRepository = newOrderNotificationRepository;
            _clients = new ConcurrentBag<StreamWriter>();
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            _newOrderNotifications = _newOrderNotificationRepository.GetAll().GetAwaiter().GetResult();

            foreach(var notification in _newOrderNotifications)
            {
                _newOrderNotificationRepository.LoadOrder(notification).GetAwaiter().GetResult();
            }
        }

        public void AddClient(StreamWriter client)
        {
            AddItemsToStream(client);

            _clients.Add(client);
        }

        private async void AddItemsToStream(StreamWriter stream)
        {
            string jsonData = "[";
            for (int i = 0; i < _newOrderNotifications.Count; i++)
            {
                jsonData += string.Format("{0}", JsonSerializer.Serialize(new { item = _newOrderNotifications[i], action = "Item added" }));

                if(i != _newOrderNotifications.Count - 1)
                {
                    jsonData += ",";
                }
            } 
            jsonData += "]";

            await stream.WriteAsync(jsonData);
            await stream.FlushAsync();
        }

        public async Task AddNotification(NewOrderNotificationModel newOrderNotification)
        {
            await _newOrderNotificationRepository.Add(newOrderNotification);
            await _newOrderNotificationRepository.LoadOrder(newOrderNotification);
            _newOrderNotifications.Add(newOrderNotification);
            await SendToClients(newOrderNotification, "Item added");
        }

        public async Task RemoveNotification(NewOrderNotificationModel newOrderNotification)
        {
            _newOrderNotifications.Remove(newOrderNotification);
            await _newOrderNotificationRepository.Remove(newOrderNotification);
            await SendToClients(newOrderNotification, "Item removed");
        }

        private async Task SendToClients(NewOrderNotificationModel data, string action)
        {
            foreach (var client in _clients)
            {
                string jsonData = string.Format("[{0}]", JsonSerializer.Serialize(new { data, action }));
                await client.WriteAsync(jsonData);
                await client.FlushAsync();
            }
        }

        public void TryTake()
        {
            StreamWriter ignore;
            _clients.TryTake(out ignore);
        }
    }
}
