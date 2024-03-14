namespace TransactionManager.Application.Interfaces;

public interface ITimeZoneService
{
    DateTimeOffset GetLocalTimeByCoordinates(DateTime utcDateTime, double lat, double lng);
}