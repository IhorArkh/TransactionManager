namespace TransactionManager.Application.Exceptions;

public class CsvHelperReadException : Exception
{
    public CsvHelperReadException(string message) : base(message)
    {
    }
}