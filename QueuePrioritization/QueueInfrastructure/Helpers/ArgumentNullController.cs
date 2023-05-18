namespace QueueInfrastructure.Helpers;

public static class ArgumentNullController
{
    public static T ThrowIfNull<T>(this T instance)
    {
        return instance ?? throw new ArgumentNullException(nameof(instance));
    }
}