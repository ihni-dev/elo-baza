namespace EloBaza.ServiceBusListener.NewUserRegistered.Configurations
{
    public class NewUserRegisteredServiceBusConfig
    {
        public string ConnectionString { get; set; } = default!;
        public string TopicName { get; set; } = default!;
        public string SubscriptionName { get; set; } = default!;
    }
}