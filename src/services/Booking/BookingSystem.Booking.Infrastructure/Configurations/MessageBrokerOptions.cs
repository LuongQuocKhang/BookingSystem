namespace BookingSystem.Booking.Infrastructure.Configurations;

public class MessageBrokerOptions
{
    public static readonly string BindLocator = "MessageBrokers";

    public RabbitMQSetting? Notification { get; set; }
}

public class RabbitMQSetting
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string VirtualHost { get; set; } = string.Empty;
}
