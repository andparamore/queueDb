using QueueInfrastructure.Models.Enum;

namespace QueueInfrastructure.Models.Models;

public class RequestDto
{
    public RequestTypesEnum RequestType { get; set; }
    
    public int CurrentStep { get; set; }

    public Guid InitialRequestId { get; set; }
    
    public Guid PreviousRequestId { get; set; }

    public string Payload { get; set; } = string.Empty;
}