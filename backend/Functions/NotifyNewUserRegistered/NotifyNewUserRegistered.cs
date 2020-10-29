using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NotifyNewUserRegistered
{
    public static class NotifyNewUserRegistered
    {
        [FunctionName("notify-new-user-registered")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [ServiceBus("new-user-registered", Connection = "ServiceBusConnection")] MessageSender messageSender,
            ILogger log)
        {
            log.LogInformation("New user register function called.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var bytes = Encoding.ASCII.GetBytes(requestBody);
            var message = new Message(bytes);

            await messageSender.SendAsync(message);
        }
    }
}
