using TransactionManager.Application.TransactionRecord.Queries.DTOs;

namespace TransactionManager.Application.Interfaces;

public interface ITransactionRecordsService
{
    Task<IEnumerable<TransactionOccuredInClientsTimeZoneDto>> GetTransactionsOccuredInClientsTimeZone(int year,
        int month = default);

    Task<IEnumerable<TransactionOccuredInUsersTimeZoneDto>> GetTransactionsOccuredInUsersTimeZone(int year,
        int month = default);
}