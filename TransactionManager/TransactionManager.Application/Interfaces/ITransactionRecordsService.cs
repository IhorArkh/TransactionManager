namespace TransactionManager.Application.Interfaces;

public interface ITransactionRecordsService
{
    Task<IEnumerable<Domain.TransactionRecord>> GetTransactionsOccuredInClientsTimeZone(int year, int month = default);
    Task<IEnumerable<Domain.TransactionRecord>> GetTransactionsOccuredInUsersTimeZone(int year, int month = default);
}