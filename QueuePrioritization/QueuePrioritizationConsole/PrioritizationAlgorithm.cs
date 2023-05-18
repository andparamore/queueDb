namespace QueuePrioritizationConsole;

public static class PrioritizationAlgorithm
{
    public static List<RequestTypeModel> Processing(this List<RequestModel> requestModels, List<RequestTypeModel> requestTypeModels)
    {
        foreach (var typeModel in requestTypeModels)
        {
            var typeId = typeModel.Id;
            typeModel.CalculateTotalWeight(requestModels
                    .Count(r => r.RequestTypeId == typeId));
        }
        
        foreach (var requestModel in requestModels)
        {
            requestModel.CalculatePriority(requestTypeModels.First(x => x.Id == requestModel.RequestTypeId).Weight);
        }

        var priorityTypeId = requestTypeModels.OrderBy(x => x.TotalWeight).Last().Id;

        var priorityRequest = requestModels.Where(x => x.RequestTypeId == priorityTypeId).OrderBy(x => x.CreateDate).First();

        _ = requestModels.Remove(priorityRequest);

        return requestTypeModels;
    }

    private static RequestTypeModel CalculateTotalWeight(this RequestTypeModel typeModel, int count)
    {
        typeModel.TotalWeight = typeModel.Weight * count;
        return typeModel;
    }
    
    private static void CalculatePriority(this RequestModel requestModel, int weight)
    {
        requestModel.Priority += weight;
    }
}