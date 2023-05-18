namespace QueueBuilder.Config;

public class QueueBuilderBusinessConfiguration
{
    public const string SectionName = "QueueBuilderBusinessConfiguration";

    public IList<RequestTypeConfiguration> RequestTypes { get; set; } = new List<RequestTypeConfiguration>();
}