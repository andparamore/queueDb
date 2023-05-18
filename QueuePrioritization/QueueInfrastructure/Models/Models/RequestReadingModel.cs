using QueueInfrastructure.Models.Enum;

namespace QueueInfrastructure.Models.Models;

public class RequestReadingModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public RequestTypesEnum RequestType { get; set; }
    
    public int CurrentStep { get; set; }

    public string Payload { get; set; } = string.Empty;
}