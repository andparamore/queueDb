using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Enum;

namespace QueueInfrastructure.Models.Models;

public class RequestModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public RequestTypesEnum RequestType { get; set; }

    public int CurrentStep { get; set; } = 1;

    public Status Status { get; set; } = Status.Pending;
    
    public Guid InitialRequestId { get; set; }
    
    public Guid? PreviousRequestId { get; set; }

    public string Payload { get; set; } = string.Empty;

    public int Attempts { get; set; }
    
    public double Weight { get; set; }
    
    public double TimeStampArrived { get; set; }
}