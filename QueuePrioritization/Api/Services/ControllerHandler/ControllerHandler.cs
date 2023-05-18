using System.Diagnostics;
using QueueApi.QueueProcessing;
using QueueBuilder.Config;
using QueueBuilder.Services.ConfigurationHandler;
using QueueBuilder.Services.QueueHandler;
using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Models;
using RequestBroker.Services.Processing;

namespace QueueApi.Services.ControllerHandler;

public class ControllerHandler : IControllerHandler
{
    private readonly IProcessingService _processingService;

    private readonly IQueueApiHandler _apiHandler;
    
    private readonly IQueueBuilderHandler _builderHandler;
    
    private readonly IConfigurationHandler _configurationHandler;
    
    
    public ControllerHandler(IProcessingService processingService, IQueueApiHandler apiHandler, IQueueBuilderHandler builderHandler, IConfigurationHandler configurationHandler)
    {
        _processingService = processingService;
        _apiHandler = apiHandler;
        _builderHandler = builderHandler;
        _configurationHandler = configurationHandler;
    }
    
    public async Task Handle()
    {
        await _processingService.ProcessAsync();
    }

    public async Task<int> HandlingMultipleRequests(int minutes)
    {
        var timer = new Stopwatch();

        try
        {
            timer.Start();
            var count = 0;
            var timeSpan = new TimeSpan(0, minutes, 0);
            while (timer.Elapsed <= timeSpan)
            {
                await _processingService.ProcessAsync();
                Console.WriteLine(timer.Elapsed);
                count++;
            }
            return count;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            timer.Stop();
        }
    }

    public async Task AddRequest(RequestDto requestDto)
    {
        await _builderHandler.AddRequest(requestDto);
    }

    public async Task UpdatePriorityRequest(WeightUpdateDto dto)
    {
        await _apiHandler.UpdatePriorityRequest(dto);
    }

    public async Task<IEnumerable<RequestModel>> GetRequests()
    {
        return await _apiHandler.GetAllRequests();
    }

    public async Task<IEnumerable<RequestTypeConfiguration>> GetTypesConfiguration()
    {
        return await _configurationHandler.GetTypesConfiguration();
    }

    public async Task AddRequestType(RequestTypeConfigurationModel requestTypeConfigurationModel)
    {
        await _configurationHandler.AddRequestType(requestTypeConfigurationModel);
    }
}