using QueueBuilder.Repositories;
using QueueBuilder.Services.ConfigurationHandler;
using QueueInfrastructure.Helpers;
using QueueInfrastructure.Models.Models;

namespace QueueBuilder.Services.WeightDistribution;

public class WeightDistributionService : IWeightDistributionService
{
    private readonly IConfigurationHandler _configurationHandler;
    
    public WeightDistributionService(IConfigurationHandler configurationHandler)
    {
        _configurationHandler = configurationHandler;
    }
    
    public async Task<RequestModel> MapToRequestModel(RequestDto dto)
    {
        try
        {
            var guid = Guid.NewGuid();
            return new RequestModel
            {
                Id = guid,
                RequestType = dto.RequestType,
                CurrentStep = dto.CurrentStep,
                InitialRequestId = dto.CurrentStep == 1 ? guid : dto.InitialRequestId,
                PreviousRequestId = null,
                Payload = dto.Payload,
                TimeStampArrived = UnixTimestampHelper.GetUnixTimestampNow(),
                Weight = await _configurationHandler.GetWeightByType(dto.RequestType, dto.CurrentStep)
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}