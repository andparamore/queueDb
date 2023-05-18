using QueueInfrastructure.Models.Enum;

namespace QueueInfrastructure.Models.Config;

public class RequestTypeConfigurationModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public RequestTypesEnum TypeName { get; set; }
    
    public double Weight { get; set; }

    public IEnumerable<StepConfigurationModel>? Steps { get; set; }
}