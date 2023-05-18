namespace QueueInfrastructure.Helpers;

public static class UnixTimestampHelper
{
    public static double GetUnixTimestampNow() => 
        DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
}