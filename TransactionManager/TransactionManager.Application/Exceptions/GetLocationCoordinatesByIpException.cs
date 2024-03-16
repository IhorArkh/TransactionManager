namespace TransactionManager.Application.Exceptions;

public class GetLocationCoordinatesByIpException : Exception
{
    public GetLocationCoordinatesByIpException(string message) : base(message)
    {
    }
}