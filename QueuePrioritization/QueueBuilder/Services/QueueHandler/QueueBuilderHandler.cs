using QueueBuilder.Repositories;
using QueueBuilder.Services.ConfigurationHandler;
using QueueBuilder.Services.WeightDistribution;
using QueueInfrastructure.Helpers;
using QueueInfrastructure.Models.Models;

namespace QueueBuilder.Services.QueueHandler;

public class QueueBuilderHandler : IQueueBuilderHandler
{
    private readonly IConfigurationHandler _configurationHandler;
    
    private readonly IQueueBuilderRepository _requestRepository;

    private readonly IWeightDistributionService _weightDistributionService;

    public QueueBuilderHandler(IQueueBuilderRepository requestRepository, IWeightDistributionService weightDistributionService, IConfigurationHandler configurationHandler)
    {
        _requestRepository = requestRepository;
        _weightDistributionService = weightDistributionService;
        _configurationHandler = configurationHandler;
    }
    
    public async Task<int> CreateNewRequest(RequestModelView request)
    {
        var totalStep = await _requestRepository.GetTotalStepByType(request.RequestType);
        if (request.CurrentStep < totalStep)
        {
            await _requestRepository.AddRequest(new RequestModel
            {
                RequestType = request.RequestType,
                CurrentStep = request.CurrentStep + 1,
                InitialRequestId = request.InitialRequestId,
                PreviousRequestId = request.Id,
                Payload = $"{request.Payload}1",
                TimeStampArrived = UnixTimestampHelper.GetUnixTimestampNow(),
                Weight = await _configurationHandler.GetWeightByType(request.RequestType, request.CurrentStep)
            });
        }
        /*
        else
        {
            var id = Guid.NewGuid();
            await _requestRepository.AddRequest(new RequestModel
            {
                Id = id,
                RequestType = request.RequestType,
                CurrentStep = 1,
                InitialRequestId = id,
                PreviousRequestId = null,
                Payload = "s",
                TimeStampArrived = UnixTimestampHelper.GetUnixTimestampNow(),
                Weight = await _configurationHandler.GetWeightByType(request.RequestType, request.CurrentStep)
            });
        }
        */

        return totalStep;
    }
    
    public async Task RemoveOldRequest(RequestModelView request)
    {
        try
        {
            var totalStep = await _requestRepository.GetTotalStepByType(request.RequestType);
            var requestModel = await _requestRepository.GetRequestById(request.Id);
            for (int i = 0; i < totalStep; i++)
            { 
                await _requestRepository.RemoveRequest(requestModel); 
                if (requestModel.PreviousRequestId != null && requestModel.Status == Status.Completed)
                { 
                    requestModel = await _requestRepository.GetRequestById(requestModel.PreviousRequestId ?? throw new ArgumentNullException(nameof(requestModel.PreviousRequestId)));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddRequest(RequestDto dto)
    {
        await _requestRepository.AddRequest(await _weightDistributionService.MapToRequestModel(dto));
    }

    public async Task StatusToCompleted(RequestModelView requestModel)
    {
        await _requestRepository.UpdateStatusRequest(requestModel, Status.Completed);
    }
}