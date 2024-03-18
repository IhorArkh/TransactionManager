namespace TransactionManager.Application.Exceptions;

public class CsvHelperReadException : BusinessLogicException
{
    public CsvHelperReadException(string message) : base(message)
    {
    }
}