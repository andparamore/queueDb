using QueueApi.Repositories;
using QueueInfrastructure.Models.Models;

namespace QueueApi.QueueProcessing;

public class QueueApiHandler : IQueueApiHandler
{
    private readonly IQueueApiRepository _repository;
    

    public QueueApiHandler(IQueueApiRepository repository)
    {
        _repository = repository;
    }

    public async Task UpdatePriorityRequest(WeightUpdateDto dto)
    {
        var requestModel = await _repository.GetRequestById(dto.Id);

        requestModel.Weight = dto.Weight;

        await _repository.UpdateRequest(requestModel);
    }

    public async Task<RequestModelView> GetPriorityRequest()
    {
        return await _repository.GetPriorityRequest();
    }

    public async Task<IEnumerable<RequestModel>> GetAllRequests()
    {
        return await _repository.GetRequests();
    }

    public async Task ExpiryDateCheck()
    {
        try
        {
            await _repository.ExpiryDateCheck();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<IEnumerable<RequestModelView>> GetPendingRequests()
    {
        return await _repository.GetPendingRequest();
    }

    /*
    
    private async Task ErrorHandling()
    {
        var previousRequestsChain = new List<RequestModel>();
        previousRequestsChain.AddRange(await _repository.GetRequestsFromChain());
        var initialRequestsIds = GetGuidsFromChain(previousRequestsChain);
        foreach (var id in initialRequestsIds)
        {
            await UpdateRange(id);
        }

        await _repository.RemoveRangeRequest(previousRequestsChain);
    }

    private List<Guid> GetGuidsFromChain(List<RequestModel> previousRequestsChain)
    {
        var initialRequestsIds = new List<Guid>();
        previousRequestsChain.ForEach(x =>
        {
            if (!initialRequestsIds.Contains(x.InitialRequestId))
                initialRequestsIds.Add(x.InitialRequestId);
        });
        return initialRequestsIds;
    }

    private async Task UpdateRange(Guid id)
    {
        try
        {
            var requestForUpdate = await _repository.GetRequestById(id);
            await _repository.UpdateRequest(new RequestModel
            {
                Id = requestForUpdate.InitialRequestId,
                RequestTypeId = requestForUpdate.RequestTypeId,
                CurrentStep = requestForUpdate.CurrentStep,
                TotalStep = requestForUpdate.TotalStep,
                InitialRequestId = requestForUpdate.InitialRequestId,
                PreviousRequestId = null,
                Payload = requestForUpdate.Payload,
                TimeStampArrived = requestForUpdate.TimeStampArrived,
                Weight = Convert.ToInt32(requestForUpdate.RequestTypeId.GetEnumDescription())
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    */
}