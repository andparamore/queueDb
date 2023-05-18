using QueueBuilder.Config;
using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Models;

namespace QueueApi.Services.ControllerHandler;

public interface IControllerHandler
{
    Task Handle();

    Task<int> HandlingMultipleRequests(int timeSpan);

    Task ParallelHandlingMultipleRequests(int loopsCount);

    Task AddRequest(RequestDto requestDto);

    Task UpdatePriorityRequest(WeightUpdateDto dto);

    Task<IEnumerable<RequestModel>> GetRequests();

    Task<IEnumerable<RequestTypeConfiguration>> GetTypesConfiguration();
    
    Task AddRequestType(RequestTypeConfigurationModel requestTypeConfigurationModel);
    
    Task<IEnumerable<RequestModelView>> GetPendingRequests();
}