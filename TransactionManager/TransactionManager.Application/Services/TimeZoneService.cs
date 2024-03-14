using GeoTimeZone;
using TimeZoneConverter;
using TransactionManager.Application.Interfaces;

namespace TransactionManager.Application.Services;

public class TimeZoneService : ITimeZoneService
{
    public DateTimeOffset GetLocalTimeByCoordinates(DateTime utcDateTime, double lat, double lng)
    {
        string tzIana = TimeZoneLookup.GetTimeZone(lat, lng).Result;
        TimeZoneInfo tzInfo = TZConvert.GetTimeZoneInfo(tzIana);
        DateTimeOffset convertedTime = TimeZoneInfo.ConvertTime(utcDateTime, tzInfo);

        return convertedTime;
    }
}