using QueueInfrastructure.Models.Models;

namespace QueueBuilder.Services.QueueHandler;

public interface IQueueBuilderHandler
{
    Task<int> CreateNewRequest(RequestModelView request);

    Task RemoveOldRequest(RequestModelView request);

    Task AddRequest(RequestDto dto);
    
    Task StatusToCompleted(RequestModelView requestModel);
}