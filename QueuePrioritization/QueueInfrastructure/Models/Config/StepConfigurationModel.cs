namespace QueueInfrastructure.Models.Config;

public class StepConfigurationModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public int StepNumber { get; set; }
    
    public double WeightMultiplier { get; set; }

    public RequestTypeConfigurationModel? RequestTypeConfigurationModel { get; set; }

    public Guid RequestTypeId { get; set; }
}