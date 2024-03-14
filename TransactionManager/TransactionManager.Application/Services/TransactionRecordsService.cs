using TransactionManager.Application.Extensions;
using TransactionManager.Application.Interfaces;
using TransactionManager.Persistence;

namespace TransactionManager.Application.Services;

public class TransactionRecordsService : ITransactionRecordsService
{
    private readonly DapperContext _dapperContext;
    private readonly ITimeZoneService _timeZoneService;
    private readonly ILocationService _locationService;

    public TransactionRecordsService(DapperContext dapperContext, ITimeZoneService timeZoneService,
        ILocationService locationService)
    {
        _dapperContext = dapperContext;
        _timeZoneService = timeZoneService;
        _locationService = locationService;
    }

    public async Task<IEnumerable<Domain.TransactionRecord>> GetTransactionsOccuredInClientsTimeZone(int year,
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

            if (convertedTime.Year == year && month == default)
                validTransactions.Add(transaction);
            else if (convertedTime.Year == year && convertedTime.Month == month)
                validTransactions.Add(transaction);
        }

        return validTransactions;
    }

    public async Task<IEnumerable<Domain.TransactionRecord>> GetTransactionsOccuredInUsersTimeZone(int year,
        int month = default)
    {
        var transactions = month == default
            ? await _dapperContext.GetTransactionRecordsByYear(year)
            : await _dapperContext.GetTransactionRecordsByMonth(year, month);

        var validTransactions = new List<Domain.TransactionRecord>();

        var location = _locationService.GetLocationCoordinatesByIp();

        var coordinates = location.SplitCoordinatesIntoDouble();
        if (coordinates.lat == default || coordinates.lng == default)
            throw new Exception("Error during converting coordinates.");

        foreach (var transaction in transactions)
        {
            var convertedTime = _timeZoneService.GetLocalTimeByCoordinates(transaction.TransactionDate,
                coordinates.lat, coordinates.lng);

            if (convertedTime.Year == year && month == default)
                validTransactions.Add(transaction);
            else if (convertedTime.Year == year && convertedTime.Month == month)
                validTransactions.Add(transaction);
        }

        return validTransactions;
    }
}