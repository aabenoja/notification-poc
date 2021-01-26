using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace NotificationPoc.Processors
{
  public static class NotificationBusDeadLetter
    {
        [FunctionName("NotificationBusDeadLetter")]
        public static void Run([ServiceBusTrigger("testsend/$DeadLetterQueue", Connection = "")]string myQueueItem, ILogger log)
        {
            var data = JsonSerializer.Deserialize<TestData>(myQueueItem);
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
