namespace TransactionManager.Application.Interfaces;

public interface ITransactionRecordsTimeZoneService
{
    Task<IEnumerable<Domain.TransactionRecord>> GetTransactionRecordsInClientLocalTime(int year);
}