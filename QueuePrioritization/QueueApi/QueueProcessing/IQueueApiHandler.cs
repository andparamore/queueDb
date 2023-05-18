using QueueInfrastructure.Models.Models;

namespace QueueApi.QueueProcessing;

public interface IQueueApiHandler
{
    Task UpdatePriorityRequest(WeightUpdateDto dto);
    
    Task<RequestModelView> GetPriorityRequest();
    
    Task<IEnumerable<RequestModel>> GetAllRequests();
    
    Task ExpiryDateCheck();
}