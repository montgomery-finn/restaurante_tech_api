using Domain.Models;
using Domain.Repositories;
using Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace restaurante_tech_api.Services
{
    public class NewOrderNotificationService
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

        private async void LoadNotifications()
        {
            _newOrderNotifications = await _newOrderNotificationRepository.GetAll();
        }

        public void AddClient(StreamWriter client)
        {
            AddItemsToStream(client);

            _clients.Add(client);
        }

        private async void AddItemsToStream(StreamWriter stream)
        {
            string jsonData = "";
            foreach (var item in _newOrderNotifications)
            {
                jsonData += string.Format("{0}\n", JsonSerializer.Serialize(new { item, action = "item loaded" }));
            }

            await stream.WriteAsync(jsonData);
            await stream.FlushAsync();
        }

        public async Task AddNotification(NewOrderNotificationModel newOrderNotification)
        {
            _newOrderNotifications.Add(newOrderNotification);
            await _newOrderNotificationRepository.Add(newOrderNotification);
            await SendToClients(newOrderNotification, "Item added");
        }

        private async Task SendToClients(NewOrderNotificationModel data, string action)
        {
            foreach (var client in _clients)
            {
                string jsonData = string.Format("{0}\n", JsonSerializer.Serialize(new { data, action }));
                await client.WriteAsync(jsonData);
                await client.FlushAsync();
            }
        }
    }
}
