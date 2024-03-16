using TransactionManager.Application.Exceptions;
using TransactionManager.Application.Extensions;
using TransactionManager.Application.Interfaces;
using TransactionManager.Application.TransactionRecord.Queries.DTOs;
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

    public async Task<IEnumerable<TransactionOccuredInClientsTimeZoneDto>> GetTransactionsOccuredInClientsTimeZone(
        int year,
        int month = default)
    {
        var transactions = month == default
            ? await _dapperContext.GetTransactionsByYear(year)
            : await _dapperContext.GetTransactionsByMonth(year, month);

        if (transactions.Count() == default)
            return new List<TransactionOccuredInClientsTimeZoneDto>();

        var transactionDtos = transactions.Select(x =>
            new TransactionOccuredInClientsTimeZoneDto
            {
                TransactionRecordId = x.TransactionRecordId,
                Amount = x.Amount,
                ClientLocation = x.ClientLocation,
                Email = x.Email,
                Name = x.Name,
                TransactionDate = x.TransactionDate
            }).ToList();

        var validTransactions = new List<TransactionOccuredInClientsTimeZoneDto>();

        foreach (var transaction in transactionDtos)
        {
            var coordinates = transaction.ClientLocation.SplitCoordinatesIntoDouble();

            if (coordinates.lat == default || coordinates.lng == default)
                throw new Exception("Error during converting coordinates.");

            var convertedTime = _timeZoneService.GetLocalTimeByCoordinates(transaction.TransactionDate,
                coordinates.lat, coordinates.lng);

            if (convertedTime.Year == year && month == default)
            {
                transaction.ClientsDateTime = convertedTime.DateTime;
                validTransactions.Add(transaction);
            }
            else if (convertedTime.Year == year && convertedTime.Month == month)
            {
                transaction.ClientsDateTime = convertedTime.DateTime;
                validTransactions.Add(transaction);
            }
        }

        return validTransactions;
    }

    public async Task<IEnumerable<TransactionOccuredInUsersTimeZoneDto>> GetTransactionsOccuredInUsersTimeZone(int year,
        int month = default)
    {
        var transactions = month == default
            ? await _dapperContext.GetTransactionsByYear(year)
            : await _dapperContext.GetTransactionsByMonth(year, month);

        if (transactions.Count() == default)
            return new List<TransactionOccuredInUsersTimeZoneDto>();

        var transactionDtos = transactions.Select(x =>
            new TransactionOccuredInUsersTimeZoneDto
            {
                TransactionRecordId = x.TransactionRecordId,
                Amount = x.Amount,
                ClientLocation = x.ClientLocation,
                Email = x.Email,
                Name = x.Name,
                TransactionDate = x.TransactionDate
            }).ToList();

        var validTransactions = new List<TransactionOccuredInUsersTimeZoneDto>();

        var location = _locationService.GetLocationCoordinatesByIp();
        if (location == default)
            throw new GetLocationCoordinatesByIpException("Error during getting your location coordinates.");

        var coordinates = location.SplitCoordinatesIntoDouble();
        if (coordinates.lat == default || coordinates.lng == default)
            throw new Exception("Error during converting coordinates.");

        foreach (var transaction in transactionDtos)
        {
            var convertedTime = _timeZoneService.GetLocalTimeByCoordinates(transaction.TransactionDate,
                coordinates.lat, coordinates.lng);

            if (convertedTime.Year == year && month == default)
            {
                transaction.YourDateTime = convertedTime.DateTime;
                validTransactions.Add(transaction);
            }
            else if (convertedTime.Year == year && convertedTime.Month == month)
            {
                transaction.YourDateTime = convertedTime.DateTime;
                validTransactions.Add(transaction);
            }
        }

        return validTransactions;
    }
}