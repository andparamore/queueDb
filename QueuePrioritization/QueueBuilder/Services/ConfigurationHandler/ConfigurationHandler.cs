using System.Diagnostics.CodeAnalysis;
using QueueBuilder.Config;
using QueueBuilder.Repositories;
using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Enum;

namespace QueueBuilder.Services.ConfigurationHandler;

public class ConfigurationHandler : IConfigurationHandler
{
    private readonly IQueueBuilderRepository _builderRepository;
    
    public ConfigurationHandler(IQueueBuilderRepository builderRepository)
    {
        _builderRepository = builderRepository;
    }
    
    public async Task<IEnumerable<RequestTypeConfiguration>> GetTypesConfiguration()
    {
        try
        {
            return (await _builderRepository.GetTypesConfiguration()).Select(MapToRequestTypeConfiguration).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddRequestType(RequestTypeConfigurationModel requestTypeConfigurationModel)
    {
        await _builderRepository.AddRequestType(requestTypeConfigurationModel);
    }

    public async Task<double> GetWeightByType(RequestTypesEnum type, int stepNumber)
    {
        var config = await _builderRepository.GetWeightByType(type);

        if (config.Steps == null)
        {
            throw new ArgumentNullException(nameof(config));
        }
        
        var weightMultiplier = config.Steps.First(x => x.StepNumber == stepNumber).WeightMultiplier;

        return config.Weight * weightMultiplier;
    }

    private RequestTypeConfiguration MapToRequestTypeConfiguration(RequestTypeConfigurationModel type)
    {
        return new RequestTypeConfiguration()
        {
            Weight = type.Weight,
            Steps = type.Steps.Select(MapToStepConfiguration),
            TypeName = type.TypeName
        };
    }

    private StepConfiguration MapToStepConfiguration(StepConfigurationModel step)
    {
        return new StepConfiguration
        {
            StepNumber = step.StepNumber,
            WeightMultiplier = step.WeightMultiplier
        };
    }
}