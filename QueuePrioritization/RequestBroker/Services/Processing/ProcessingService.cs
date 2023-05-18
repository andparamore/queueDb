using QueueApi.QueueProcessing;
using QueueBuilder.Services.QueueHandler;
using QueueInfrastructure.Models.Exceptions;
using RequestBroker.Services.ErrorHandler;

namespace RequestBroker.Services.Processing;

public class ProcessingService : IProcessingService
{
    private readonly IErrorHandler _errorHandler;

    private readonly IQueueApiHandler _apiHandler;

    private readonly IQueueBuilderHandler _builderHandler;

    public ProcessingService(IErrorHandler errorHandler, IQueueBuilderHandler builderHandler, IQueueApiHandler apiHandler)
    {
        _errorHandler = errorHandler;
        _builderHandler = builderHandler;
        _apiHandler = apiHandler;
    }

    public async Task ProcessAsync()
    {
        try
        {
            await _apiHandler.ExpiryDateCheck();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        var requestModel = await _apiHandler.GetPriorityRequest();
        
        try
        {
            //await SendAsync();
        }
        catch (EtranException e)
        {
            await _errorHandler.ErrorHandling(requestModel);
            Console.WriteLine(e);
        }
        
        await _builderHandler.StatusToCompleted(requestModel);
        var totalStep = await _builderHandler.CreateNewRequest(requestModel);
        if (requestModel.CurrentStep == totalStep)
            await _builderHandler.RemoveOldRequest(requestModel);
    }

    private async Task SendAsync()
    {
        await Task.Delay(500);
    }
}