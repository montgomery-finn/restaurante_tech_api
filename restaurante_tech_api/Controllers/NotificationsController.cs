using Microsoft.AspNetCore.Mvc;
using restaurante_tech_api.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace restaurante_tech_api.Controllers
{
    [Route("Notifications")]
    public class NotificationsController : Controller
    {
        private static ConcurrentBag<StreamWriter> _clients = new ConcurrentBag<StreamWriter>();

        private static List<Item> _itens = new List<Item>();


        [HttpPost]
        public async Task<ActionResult<Item>> Post([FromBody] Item value)
        {
            if (value == null)
                return BadRequest();

            if (value.Id == 0)
            {
                var max = _itens.Count == 0 ? 0 : _itens.Max(i => i.Id);
                value.Id = max + 1;
            }

            _itens.Add(value);

            await WriteOnStream(value, "Item added");

            return value;
        }

        [HttpGet]
        public IActionResult Streaming()
        {
            return new StreamResult(
                (stream, cancelToken) => {
                    var wait = cancelToken.WaitHandle;
                    var client = new StreamWriter(stream);

                    AddItemsToStream(client);

                    _clients.Add(client);

                    wait.WaitOne();

                    StreamWriter ignore;
                    _clients.TryTake(out ignore);
                },
                HttpContext.RequestAborted);
        }

        public class Item
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public bool IsComplete { get; set; }

            public override string ToString() => $"{Id} - {Name} - {IsComplete}";
        }


        private async Task WriteOnStream(Item data, string action)
        {
            foreach (var client in _clients)
            {
                string jsonData = string.Format("{0}\n", JsonSerializer.Serialize(new { data, action }));
                await client.WriteAsync(jsonData);
                await client.FlushAsync();
            }
        }

        private async void AddItemsToStream(StreamWriter stream)
        {
            string jsonData = "";
            foreach(var item in _itens)
            {
                jsonData += string.Format("{0}\n", JsonSerializer.Serialize(new { item, action = "item loaded" }));
            }

            await stream.WriteAsync(jsonData);
            await stream.FlushAsync();
        }
    }
}
