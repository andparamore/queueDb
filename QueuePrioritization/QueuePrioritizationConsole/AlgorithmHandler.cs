namespace QueuePrioritizationConsole;

public class AlgorithmHandler
{
    public AlgorithmHandler(int iterations, List<RequestTypeModel> requestTypeModels)
    {
        var requestModels = new List<RequestModel>();
        for (int i = 0; i < iterations; i++)
        {
            if (i % 10000 == 0)
                requestModels.AddRange(new List<RequestModel>
                {
                    new RequestModel()
                    {
                        RequestTypeId = 1
                    },
                    new RequestModel()
                    {
                        RequestTypeId = 2
                    },
                    new RequestModel()
                    {
                        RequestTypeId = 3
                    }
            });
            else if (i % 1 == 0)
            {
                requestModels.Add(new RequestModel()
                {
                    RequestTypeId = 3
                });
            }
            
            requestTypeModels = requestModels.Processing(requestTypeModels);
            var array = requestModels.ToArray();
            for (int j = 0; j < array.Count(); j++)
            {
                Console.Write(array[j].Priority + "_" + array[j].RequestTypeId + "  ");
            }
            Console.WriteLine("");
            Console.WriteLine("______________________________________________");
        }
    }
}