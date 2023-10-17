namespace gRPC.Unary.Server.Helpers;

public class Clock : IClock
{
    public DateTimeOffset Current()
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTimeOffset.UtcNow, "Europe/Warsaw");
    }
}