using System.ComponentModel;

namespace QueueInfrastructure.Models.Enum;

public enum ScaleStepsEnum
{
    [Description("1")]
    FirstStep,
    
    [Description("2")]
    SecondStep,
    
    [Description("4")]
    ThirdStep,
    
    [Description("3")]
    FourthStep,
    
    [Description("5")]
    FifthStep
}