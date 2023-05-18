namespace QueueInfrastructure.Models.Exceptions;

[Serializable]
public class EtranException : Exception
{
    public EtranException() {  }

    public EtranException(string message)
        : base(message)
    {

    }
}