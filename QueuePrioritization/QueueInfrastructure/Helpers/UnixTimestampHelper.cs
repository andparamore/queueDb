namespace QueueInfrastructure.Helpers;

public static class UnixTimestampHelper
{
    public static double GetUnixTimestampNow() => 
        DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
}