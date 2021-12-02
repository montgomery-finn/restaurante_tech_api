using Microsoft.AspNetCore.Mvc;
using restaurante_tech_api.Results;
using restaurante_tech_api.Services.Interfaces;
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
        private readonly INewOrderNotificationService _newOrderNotificationService;

        public NotificationsController(INewOrderNotificationService newOrderNotificationService)
        {
            _newOrderNotificationService = newOrderNotificationService;
        }

        [HttpGet]
        public IActionResult Streaming()
        {
            return new StreamResult(
                (stream, cancelToken) => {
                    var wait = cancelToken.WaitHandle;
                    var client = new StreamWriter(stream);

                    _newOrderNotificationService.AddClient(client);

                    wait.WaitOne();

                    _newOrderNotificationService.TryTake();
                },
                HttpContext.RequestAborted);
        }
    }
}