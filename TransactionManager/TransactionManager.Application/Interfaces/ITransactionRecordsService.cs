namespace TransactionManager.Application.Interfaces;

public interface ITransactionRecordsService
{
    Task<IEnumerable<Domain.TransactionRecord>> GetTransactionRecordsInClientLocalTime(int year, int month = 0);
}