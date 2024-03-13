using System.Globalization;
using GeoTimeZone;
using TimeZoneConverter;
using TransactionManager.Application.Interfaces;
using TransactionManager.Persistence;

namespace TransactionManager.Application.Services;

public class TransactionRecordsTimeZoneService : ITransactionRecordsTimeZoneService
{
    private readonly DapperContext _dapperContext;

    public TransactionRecordsTimeZoneService(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<Domain.TransactionRecord>> GetTransactionRecordsInClientLocalTime(int year)
    {
        var transactions = await _dapperContext.GetTransactionRecordsByYear(year);

        var validTransactions = new List<Domain.TransactionRecord>();

        foreach (var transaction in transactions)
        {
            var coordinates = transaction.ClientLocation.Split(',');
            double.TryParse(coordinates[0].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lat);
            double.TryParse(coordinates[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lng);
            //TODO handle it somehow

            string tzIana = TimeZoneLookup.GetTimeZone(lat, lng).Result;
            TimeZoneInfo tzInfo = TZConvert.GetTimeZoneInfo(tzIana);
            DateTimeOffset convertedTime = TimeZoneInfo.ConvertTime(transaction.TransactionDate, tzInfo);

            if (convertedTime.Year == year)
                validTransactions.Add(transaction);
        }

        return validTransactions;
    }
}