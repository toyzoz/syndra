namespace EventBusRabbitMQ;

public class EventBusOptions
{
    public const string SectionName = "EventBus";
    public string SubscriptionClientName { get; set; }
    public int RetryCount { get; set; } = 10;
}
