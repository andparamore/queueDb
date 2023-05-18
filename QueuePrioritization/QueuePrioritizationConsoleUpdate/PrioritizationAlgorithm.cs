namespace QueuePrioritizationConsoleUpdate;

public static class PrioritizationAlgorithm
{
    public static List<RequestModel> Processing(this List<RequestModel> requestModels, List<RequestTypeModel> requestTypeModels)
    {
        foreach (var requestModel in requestModels)
        {
            requestModel.CalculatePriority(requestTypeModels.First(x => x.Id == requestModel.RequestTypeId).Weight);
        }

        var orderedPriorityRequests = requestModels.OrderBy(x => x.Priority);
        
        var highPriority = orderedPriorityRequests.Last().Priority;
        var highPriorityRequests = orderedPriorityRequests.Where(x => x.Priority == highPriority);
        var priorityRequest = highPriorityRequests.Count() > 1
            ? highPriorityRequests.OrderBy(x => x.CreateDate).First()
            : highPriorityRequests.First();

        _ = requestModels.Remove(priorityRequest);

        return requestModels;
    }

    private static void CalculatePriority(this RequestModel requestModel, int weight)
    {
        requestModel.Priority += weight;
    }
}