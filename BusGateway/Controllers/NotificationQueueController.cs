using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BusGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly ILogger<QueueController> _logger;
        private readonly ConnectionStrings _connectionStrings;

        public QueueController(ILogger<QueueController> logger, IOptions<ConnectionStrings> connectionStrings)
        {
            _logger = logger;
            _connectionStrings = connectionStrings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Data data)
        {
            await using var client = new ServiceBusClient(_connectionStrings.ServiceBus);
            var sender = client.CreateSender("testsend");
            var messageBody = JsonSerializer.Serialize(data);
            var message = new ServiceBusMessage(messageBody);
            await sender.SendMessageAsync(message);
            return Ok();
        }
    }

    public class Data
    {
        public string Message { get; set; }
    }
}
