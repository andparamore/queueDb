using QueueInfrastructure.Models.Models;

namespace QueueApi.Repositories;

public interface IQueueApiRepository
{
    Task<RequestModel> GetRequestById(Guid id);
    
    Task<IEnumerable<RequestModel>> GetRequests();
    
    Task<IEnumerable<RequestModel>> GetRequestsFromChain();

    Task<RequestModelView> GetPriorityRequest();
    
    Task UpdateRequest(RequestModel requestModel);
    
    Task ExpiryDateCheck();
    
    Task<IEnumerable<RequestModelView>> GetPendingRequest();
}