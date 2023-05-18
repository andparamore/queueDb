using System.Collections;
using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Enum;

namespace QueueBuilder.Config;

public class RequestTypeConfiguration
{
    public RequestTypesEnum TypeName { get; set; }
    
    public double Weight { get; set; }

    public IEnumerable<StepConfiguration> Steps { get; set; } = new List<StepConfiguration>();
}