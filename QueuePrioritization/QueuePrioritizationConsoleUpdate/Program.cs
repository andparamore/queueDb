﻿// See https://aka.ms/new-console-template for more information

using QueuePrioritizationConsoleUpdate;

var requestTypeModels = new List<RequestTypeModel>
{
    new RequestTypeModel()
    {
        Id = 1,
        RequestTypeName = "type1",
        Weight = 10
    },
    new RequestTypeModel()
    {
        Id = 2,
        RequestTypeName = "type2",
        Weight = 20
    },
    new RequestTypeModel()
    {
        Id = 3,
        RequestTypeName = "type3",
        Weight = 50
    }
};

var handler = new AlgorithmHandler(100, requestTypeModels);