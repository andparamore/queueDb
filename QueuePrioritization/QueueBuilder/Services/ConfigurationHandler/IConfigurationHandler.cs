using QueueBuilder.Config;
using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Enum;

namespace QueueBuilder.Services.ConfigurationHandler;

public interface IConfigurationHandler
{
    Task<IEnumerable<RequestTypeConfiguration>> GetTypesConfiguration();
    
    Task AddRequestType(RequestTypeConfigurationModel requestTypeConfigurationModel);

    Task<double> GetWeightByType(RequestTypesEnum type, int stepNumber);
}