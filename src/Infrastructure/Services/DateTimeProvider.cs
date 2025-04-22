using Application.Common.Abstraction;

namespace Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Today()
    {
        var utcNow = DateTime.UtcNow;
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"); // or whatever you want
        return TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZone);;
    }
}