namespace TransactionManager.Application.Interfaces;

public interface ICsvHelperService
{
    public IEnumerable<T> ReadCsv<T>(Stream file);
    public byte[] WriteToCsv<T>(IEnumerable<T> data);
}