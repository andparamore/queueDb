using QueueBuilder.Config;
using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Enum;
using QueueInfrastructure.Models.Models;

namespace QueueBuilder.Repositories;

public interface IQueueBuilderRepository
{
    Task<RequestModel> GetRequestById(Guid id);

    Task<RequestTypeConfigurationModel> GetWeightByType(RequestTypesEnum type);
    
    Task<double> GetWeightMultiplierByType(RequestTypesEnum type);
    
    Task<int> GetTotalStepByType(RequestTypesEnum type);
    
    Task<IEnumerable<RequestTypeConfigurationModel>> GetTypesConfiguration();
    
    Task AddRequest(RequestModel requestModel);
    
    Task RemoveRequest(RequestModel requestModel);
    
    Task RemoveRangeRequest(IEnumerable<RequestModel> requestModel);

    Task UpdateStatusRequest(RequestModelView requestModel, Status status);
    
    Task AddRequestType(RequestTypeConfigurationModel requestTypeConfigurationModel);
}