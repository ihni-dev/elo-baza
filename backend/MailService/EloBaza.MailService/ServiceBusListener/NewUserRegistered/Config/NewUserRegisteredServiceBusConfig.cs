namespace EloBaza.MailService.ServiceBusListener.NewUserRegistered.Config
{
    public class NewUserRegisteredServiceBusConfig
    {
        public string ConnectionString { get; set; } = default!;
        public string TopicName { get; set; } = default!;
        public string SubscriptionName { get; set; } = default!;
    }
}