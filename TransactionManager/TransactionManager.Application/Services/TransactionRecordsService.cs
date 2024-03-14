using TransactionManager.Application.Extensions;
using TransactionManager.Application.Interfaces;
using TransactionManager.Persistence;

namespace TransactionManager.Application.Services;

public class TransactionRecordsService : ITransactionRecordsService
{
    private readonly DapperContext _dapperContext;
    private readonly ITimeZoneService _timeZoneService;

    public TransactionRecordsService(DapperContext dapperContext, ITimeZoneService timeZoneService)
    {
        _dapperContext = dapperContext;
        _timeZoneService = timeZoneService;
    }

    public async Task<IEnumerable<Domain.TransactionRecord>> GetTransactionRecordsInClientLocalTime(int year,
        int month = default)
    {
        var transactions = month == default
            ? await _dapperContext.GetTransactionRecordsByYear(year)
            : await _dapperContext.GetTransactionRecordsByMonth(year, month);

        var validTransactions = new List<Domain.TransactionRecord>();

        foreach (var transaction in transactions)
        {
            var coordinates = transaction.ClientLocation.SplitCoordinatesIntoDouble();

            if (coordinates.lat == default || coordinates.lng == default)
                throw new Exception("Error during converting coordinates.");

            var convertedTime = _timeZoneService.GetLocalTimeByCoordinates(transaction.TransactionDate,
                coordinates.lat, coordinates.lng);

            if (convertedTime.Year != year)
            {
                Console.WriteLine($"{transaction.TransactionRecordId} {transaction.TransactionDate} {convertedTime}");
                continue;
            }

            if (month != default && convertedTime.Month == month)
                validTransactions.Add(transaction);
        }

        return validTransactions;
    }
}