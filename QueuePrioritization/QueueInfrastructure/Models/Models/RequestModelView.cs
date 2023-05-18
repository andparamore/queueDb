using QueueInfrastructure.Models.Enum;

namespace QueueInfrastructure.Models.Models;

public class RequestModelView
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public RequestTypesEnum RequestType { get; set; }

    public int CurrentStep { get; set; }

    public Status Status { get; set; }
    
    public Guid InitialRequestId { get; set; }
    
    public double Priority { get; set; }
    
    public string Payload { get; set; } = string.Empty;
}