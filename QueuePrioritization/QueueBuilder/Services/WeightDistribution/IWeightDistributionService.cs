using QueueInfrastructure.Models.Models;

namespace QueueBuilder.Services.WeightDistribution;

public interface IWeightDistributionService
{
    Task<RequestModel> MapToRequestModel(RequestDto dto);
}